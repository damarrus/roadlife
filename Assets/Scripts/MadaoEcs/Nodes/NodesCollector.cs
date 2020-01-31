using DeRibura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MadaoEcs {
    public static class NodesCollector {

        private static Dictionary<short, HashSet<short>> componentTypeNodesTuples = new Dictionary<short, HashSet<short>>(); // Type1 - componentType, short - node type
        private static Dictionary<short, NodeMetaData> nodeFieldsStorage = new Dictionary<short, NodeMetaData>(); // short - node type

        private static short LastComponentTypeId;
        public static short LastNodeTypeId;
        public static DoubleDictionary<short, Type> NodesTypesIds = new DoubleDictionary<short, Type>();
        public static DoubleDictionary<short, Type> ComponentsTypesIds = new DoubleDictionary<short, Type>();

        public static void RegisterNode<T>() where T : Node {
            var nodeType = typeof(T);
            RegisterNode(nodeType);
        }

        public static void RegisterAllNodes() {
            var type = typeof(Node);
            var nodeTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract).ToList();

            foreach (var nodeType in nodeTypes) {
                RegisterNode(nodeType);
            }
        }

        public static void RegisterAllComponents() {
            var type = typeof(IComponent);
            var componentTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract).ToList();

            foreach (var componentType in componentTypes) {
                RegisterComponent(componentType);
            }
        }

        private static void RegisterComponent(Type type) {
            var staticComponentTypeIdField = type.GetField("TypeId");
            var staticComponentNameField = type.GetField("COMPONENT_NAME", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var componentTypeId = LastComponentTypeId++;
            ComponentsTypesIds.AddItem(componentTypeId, type);
            staticComponentTypeIdField.SetValue(null, componentTypeId);
            staticComponentNameField.SetValue(null, type.Name);
        }

        public static void RegisterNode(Type nodeType) {
            var fields = nodeType.GetFields().Where(x => x.FieldType.GetInterfaces().Any(y => y == typeof(IComponent))).ToList();
            var staticNodeTypeIdField = nodeType.GetField("TypeId");
            var nodeTypeId = LastNodeTypeId++;
            NodesTypesIds.AddItem(nodeTypeId, nodeType);
            staticNodeTypeIdField.SetValue(null, nodeTypeId);


            foreach (var fieldType in fields.Select(x => x.FieldType)) {
                short componentTypeId = ComponentsTypesIds.GetKey(fieldType);

                if (componentTypeNodesTuples.ContainsKey(componentTypeId)) {
                    componentTypeNodesTuples[componentTypeId].Add(nodeTypeId);
                } else {
                    componentTypeNodesTuples[componentTypeId] = new HashSet<short> { nodeTypeId };
                }
            }

            nodeFieldsStorage.Add(nodeTypeId, new NodeMetaData(nodeType));
        }

        public static bool HasNode(IEntity entity, short nodeTypeId) {
            return entity.BuildedNodes[nodeTypeId] != null;
        }

        public static Node GetNode(IEntity entity, short nodeTypeId) {
            return entity.BuildedNodes[nodeTypeId];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UpdateNodesOnComponentAdd(IEntity entity, IComponent component) {
            var componentTypeId = component.GetComponentTypeId();
            if (!componentTypeNodesTuples.TryGetValue(componentTypeId, out var nodesTypes)) return;

            foreach (var nodeTypeId in nodesTypes) {
                var nodeMetaData = nodeFieldsStorage[nodeTypeId];
                var accessor = nodeMetaData.Accessor;
                var node = entity.NodesInBuild[nodeTypeId];
                if (node == null) {
                    node = nodeMetaData.CreateMethod();
                    node.Entity = entity;
                    entity.NodesInBuild[nodeTypeId] = node;
                }

                accessor[node, component.GetComponentName()] = component;
                if (IsNotFieldComponent(componentTypeId, nodeMetaData)) {
                    node.NotFieldsCount++;
                } else {
                    node.FieldsCount++;
                }
            }

            foreach (var nodeTypeId in nodesTypes) {
                var nodeMetaData = nodeFieldsStorage[nodeTypeId];
                var node = entity.NodesInBuild[nodeTypeId];
                var isNodeBuildedAlready = entity.BuildedNodes[nodeTypeId] != null;
                var isNodeFilled = IsNodeFilled(node, nodeMetaData.ComponentFieldsCount);

                if (isNodeFilled && !isNodeBuildedAlready) {
                    AddNodeToEntity(entity, nodeTypeId, node);
                } else if (!isNodeFilled && isNodeBuildedAlready) {
                    RemoveNodeFromEntity(entity, nodeTypeId, node);
                }
            }
        }

        private static bool IsNotFieldComponent(short componentTypeId, NodeMetaData nodeMetaData) {
            return nodeMetaData.NotFields.Count != 0 && nodeMetaData.NotFields.Contains(componentTypeId);
        }

        private static bool IsNodeFilled(Node node, int componentFieldsCount) {
            return node.FieldsCount == componentFieldsCount && node.NotFieldsCount == 0;
        }

        public static void UpdateNodesOnComponentRemove(IEntity entity, IComponent component) {
            var componentTypeId = component.GetComponentTypeId();
            if (!componentTypeNodesTuples.TryGetValue(componentTypeId, out var nodesTypes)) return;

            foreach (var nodeTypeId in nodesTypes) {
                var node = entity.NodesInBuild[nodeTypeId];
                var nodeMetaData = nodeFieldsStorage[nodeTypeId];

                if (IsNotFieldComponent(componentTypeId, nodeMetaData)) {
                    node.NotFieldsCount--;
                    if (IsNodeFilled(node, nodeMetaData.ComponentFieldsCount)) {
                        AddNodeToEntity(entity, nodeTypeId, node);
                    }
                } else if (entity.BuildedNodes[nodeTypeId] != null) {
                    RemoveNodeFromEntity(entity, nodeTypeId, node);
                }
            }

            foreach (var nodeTypeId in nodesTypes) {
                var nodeMetaData = nodeFieldsStorage[nodeTypeId];
                var accessor = nodeMetaData.Accessor;
                var node = entity.NodesInBuild[nodeTypeId];

                accessor[node, component.GetComponentName()] = null;
                if (!IsNotFieldComponent(componentTypeId, nodeMetaData)) {
                    node.FieldsCount--;
                }
            }
        }

        private static void RemoveNodeFromEntity(IEntity entity, short nodeTypeId, Node node) {
            entity.SendEvent(new NodeRemoveEvent(nodeTypeId));
            node.GetEntitiesWithNode().Remove(entity.Id);
            entity.BuildedNodes[nodeTypeId] = null;
        }

        private static void AddNodeToEntity(IEntity entity, short nodeTypeId, Node node) {
            entity.BuildedNodes[nodeTypeId] = node;
            node.GetEntitiesWithNode().Add(entity.Id, node);
            entity.SendEvent(new NodeAddedEvent(node.NodeTypeId));
        }
    }
}
