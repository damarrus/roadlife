using DeRibura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MadaoEcs {

    public static class Ecs {

        private static Dictionary<ulong, IEntity> entities = new Dictionary<ulong, IEntity>();
        private static Dictionary<Type, EcsSystem> systems = new Dictionary<Type, EcsSystem>(); //Type - ecs system type
        private static Dictionary<short, List<EcsSystemMethodInfo>> eventsNodeHandlers = new Dictionary<short, List<EcsSystemMethodInfo>>(); //Short - event type id, Short2 - node type

        private static short LastEventTypeId = 1;
        private static DoubleDictionary<short, Type> EventsTypesIds = new DoubleDictionary<short, Type>();
        public static int nodeAddedEventId;
        public static int nodeRemoveEventId;

        public static IEntity STUB;

        public static void SortEventHandlers() {
            eventsNodeHandlers = eventsNodeHandlers.ToDictionary(x => x.Key, x => x.Value.OrderBy(y => y.Priority).ToList());
        }

        public static void InitStubEntity() {
            STUB = CreateEntity("Stub");
            STUB.AddComponent(new EntityStubComponent());
        }

        public static void RegisterSystem<T>(bool isActive = true) where T : EcsSystem, new() {
            var newSystem = new T();
            var systemType = typeof(T);
            systems.Add(typeof(T), newSystem);
            newSystem.IsActive = isActive;

            foreach (var eventNodeHandlers in eventsNodeHandlers) {
                var eventTypeId = eventNodeHandlers.Key;
                var methodsForEvent = systemType.GetMethods()
                    .Where(x => x.GetCustomAttribute<OnEventFire>() != null && x.GetParameters()[0].ParameterType == EventsTypesIds.GetValue(eventTypeId));

                foreach (var methodForEvent in methodsForEvent) {
                    var eventNodeArgumentType = methodForEvent.GetParameters()[1].ParameterType;
                    var nodeTypeId = NodesCollector.NodesTypesIds.GetKey(eventNodeArgumentType);
                    var onEventFireAttributePriority = methodForEvent.GetCustomAttribute<OnEventFire>().Order;
                    eventNodeHandlers.Value.Add(new EcsSystemMethodInfo(nodeTypeId, onEventFireAttributePriority, newSystem, methodForEvent));
                }
            }
        }

        public static void SetSystemActive<T>(bool isActive) where T : EcsSystem {
            var systemType = typeof(T);
            var system = systems[systemType];
            system.IsActive = isActive;
        }

        public static void RegisterAllEvents() {
            var type = typeof(Event);
            var nodeTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract).ToList();

            foreach (var nodeType in nodeTypes) {
                RegisterEvent(nodeType);
            }
        }

        public static void RegisterEvent<T>() where T : Event {
            var eventType = typeof(T);
            RegisterEvent(eventType);
        }

        private static void RegisterEvent(Type eventType) {
            var staticIdField = eventType.GetField("TypeId");
            var eventTypeId = LastEventTypeId++;
            EventsTypesIds.AddItem(eventTypeId, eventType);
            staticIdField.SetValue(null, eventTypeId);

            eventsNodeHandlers.Add(eventTypeId, new List<EcsSystemMethodInfo>());

            foreach (var ecsSystemKvp in systems) {
                var ecsSystemType = ecsSystemKvp.Key;
                var methodsForEvent = ecsSystemType.GetMethods()
                    .Where(x => x.GetCustomAttribute<OnEventFire>() != null && x.GetParameters()[0].ParameterType == eventType);

                var nodesMethods = eventsNodeHandlers[eventTypeId];
                foreach (var methodForEvent in methodsForEvent) {
                    var nodeType = methodForEvent.GetParameters()[1].ParameterType;
                    var nodeTypeId = NodesCollector.NodesTypesIds.GetKey(nodeType);
                    var onEventFireAttributePriority = methodForEvent.GetCustomAttribute<OnEventFire>().Order;
                    nodesMethods.Add(new EcsSystemMethodInfo(nodeTypeId, onEventFireAttributePriority, ecsSystemKvp.Value, methodForEvent));
                }
            }

            if (eventType == typeof(NodeAddedEvent)) {
                nodeAddedEventId = eventTypeId;
            }
            if (eventType == typeof(NodeRemoveEvent)) {
                nodeRemoveEventId = eventTypeId;
            }
        }

        public static IEntity CreateEntity(string name = null) {
            var newId = EcsUidGenerator.GetNewUid();
            var newEntity = new Entity(newId, NodesCollector.LastNodeTypeId);
            newEntity.Name = name;
            RegisterEntity(newEntity);
            return newEntity;
        }

        public static void RemoveEntity(IEntity entity) {
            entity.RemoveAllComponents();
            entities.Remove(entity.Id);
        }

        public static IEntity GetEntity(ulong id) {
            return entities[id];
        }

        public static void RegisterEntity(IEntity entity) {
            entities.Add(entity.Id, entity);
            entity.IsRegistered = true;
        }

        public static void HandleComponentAdd(IComponent component, IEntity entity) {
            var groupComponent = component as GroupComponent;
            if (groupComponent != null) {
                EcsGroupController.RegisterGroupInstance(entity, groupComponent);
            }
            NodesCollector.UpdateNodesOnComponentAdd(entity, component);
        }

        public static void HandleComponentRemove(IComponent component, IEntity entity) {
            var groupComponent = component as GroupComponent;
            if (groupComponent != null) {
                EcsGroupController.RemoveGroupInstance(entity, groupComponent);
            }
            NodesCollector.UpdateNodesOnComponentRemove(entity, component);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SendEventToSystems(Event e, IEntity entity) {

#if UNITY_EDITOR
            if (e.EventTypeId == 0) {
                UnityEngine.Debug.LogError("Event not registered" + e.GetType());
            }
#endif

            var nodeHandlers = eventsNodeHandlers[e.EventTypeId];

            foreach (var nodeHandler in nodeHandlers) {
                if (e.IsCanceled) break;
                if (!nodeHandler.Source.IsActive) continue;

                var nodeTypeId = nodeHandler.NodeTypeId;
                if ((e.EventTypeId == nodeAddedEventId && nodeTypeId != ((NodeAddedEvent)e).NodeId)
                    || (e.EventTypeId == nodeRemoveEventId && nodeTypeId != ((NodeRemoveEvent)e).NodeId)) continue;

                var entityNode = NodesCollector.GetNode(entity, nodeTypeId);
                if (entityNode == null) continue;

                nodeHandler.Method.Invoke(nodeHandler.Source, e, entityNode);
            }
        }
    }
}
