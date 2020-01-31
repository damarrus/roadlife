using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MadaoEcs {
    public class EntityBehaviour : MonoBehaviour {

        [SerializeField] private bool autoRegisterOnEnable;
        [SerializeField] private string entityName;
        [SerializeField] private string config;
        [SerializeField] private List<MonoBehaviour> componentsToAdd;

        public IEntity entity { get; private set; }

        void OnEnable() {
            if (autoRegisterOnEnable) {
                Initialize();
            }
        }

        public void Initialize() {
            InnerInitialize();
        }

        private void InnerInitialize() {
            entity = string.IsNullOrEmpty(config)
                ? Ecs.CreateEntity(entityName)
                : EcsConfigUtil.CreateEntity(config);

            foreach (var item in componentsToAdd) {
                var component = item as IComponent;
                entity.AddComponent(component);
            }
        }

        void OnDisable() {
            if (entity != null) {
                Ecs.RemoveEntity(entity);
            }
        }

        void OnValidate() {
            if (componentsToAdd == null) return;

            var componentsToRemove = componentsToAdd.Where(x => !(x is IComponent)).ToList();
            foreach (var component in componentsToRemove) {
                Debug.LogError($"{component.GetType()} isn't a IComponent");
                componentsToAdd.Remove(component);
            }
        }
    }
}
