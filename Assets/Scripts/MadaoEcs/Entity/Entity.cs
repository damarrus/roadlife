using System.Collections.Generic;
using System.Linq;

namespace MadaoEcs {
    public class Entity : IEntity {
        public bool IsRegistered { get; set; }
        public ulong Id { get; private set; }
        public string Name { get; set; }
        public string ConfigPath { get; set; }
        public List<Node> NodesInBuild { get; } = new List<Node>(100);
        public List<Node> BuildedNodes { get; } = new List<Node>(100);

        private Dictionary<short, IComponent> components = new Dictionary<short, IComponent>(8); //short = Component type id

        public Entity(ulong id, int nodesCapacity) {
            Id = id;
            for (int i = 0; i < nodesCapacity; i++) {
                NodesInBuild.Add(null);
                BuildedNodes.Add(null);
            }
        }

        public T AddComponent<T>(T component) where T : IComponent {
            components.Add(component.GetComponentTypeId(), component);
            if (IsRegistered) {
                Ecs.HandleComponentAdd(component, this);
            }
            return component;
        }

        public T GetIComponent<T>(short componentTypeId) where T : IComponent {
            if (components.TryGetValue(componentTypeId, out var component)) {
                return (T)component;
            }
            return default;
        }

        public T SendEvent<T>(T eventInstance) where T : Event {
            Ecs.SendEventToSystems(eventInstance, this);
            return eventInstance;
        }

        public void RemoveComponent(short componentId) {
            var component = components[componentId];
            components.Remove(componentId);
            if (IsRegistered) {
                Ecs.HandleComponentRemove(component, this);
            }
        }

        public void RemoveComponentIfPresent(short componentId) {
            if (components.TryGetValue(componentId, out var component)) {
                components.Remove(componentId);
                if (IsRegistered) {
                    Ecs.HandleComponentRemove(component, this);
                }
            }
        }

        public bool HasComponent(short componentId) {
            return components.ContainsKey(componentId);
        }

        public void RemoveAllComponents() {
            while (components.Count > 0) {
                var componentKvp = components.First();
                components.Remove(componentKvp.Key);
                if (IsRegistered) {
                    Ecs.HandleComponentRemove(componentKvp.Value, this);
                }
            }
        }

        public List<IComponent> GetAllComponents() {
            return components.Select(x => x.Value).ToList();
        }

        public IEntity Clone() {
            var clonedEntity = Ecs.CreateEntity();
            clonedEntity.Name = Name;
            clonedEntity.ConfigPath = ConfigPath;
            foreach (var component in components.Values) {
                var clonedComponent = component.Clone();
                if (clonedComponent != null) {
                    clonedEntity.AddComponent(clonedComponent);
                }
            }
            return clonedEntity;
        }
    }
}
