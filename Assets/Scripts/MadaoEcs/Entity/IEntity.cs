using System;
using System.Collections.Generic;

namespace MadaoEcs {
    public interface IEntity {
        bool IsRegistered { get; set; }
        ulong Id { get; }
        string Name { get; set; }
        string ConfigPath { get; set; }
        List<Node> NodesInBuild { get; }
        List<Node> BuildedNodes { get; }
        T GetIComponent<T>(short componentTypeId) where T : IComponent;
        List<IComponent> GetAllComponents();
        T AddComponent<T>(T component) where T : IComponent;
        void RemoveComponent(short componentTypeId);
        void RemoveComponentIfPresent(short componentTypeId);
        void RemoveAllComponents();
        T SendEvent<T>(T eventInstance) where T : Event;
        bool HasComponent(short componentTypeId);
        IEntity Clone();

    }
}
