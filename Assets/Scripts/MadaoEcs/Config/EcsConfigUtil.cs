using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YamlDotNet.Serialization;

namespace MadaoEcs {
    public class EcsConfigUtil {

        private static Serializer yamlSerializer;
        private static Deserializer yamlDeserializer;
        private static Dictionary<string, string> yamlConfigs = new Dictionary<string, string>();
        private static Type iComponentType = typeof(IComponent);
        private static Type basicTemplateType = typeof(Template);
        private static Dictionary<string, object> deserializedCache = new Dictionary<string, object>();
        private static Dictionary<string, Template> deserializedTemplates = new Dictionary<string, Template>();

        static EcsConfigUtil() {
            yamlSerializer = new SerializerBuilder().EmitDefaults().Build();
            yamlDeserializer = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();
        }

        public static IEntity CreateEntity(string configPath) {
#if UNITY_EDITOR
            if (!deserializedCache.ContainsKey(configPath)) {
                UnityEngine.Debug.Log("Config not found " + configPath);
            }
#endif

            var cache = deserializedCache[configPath];
            var entity = ((IEntity)cache).Clone();
            entity.ConfigPath = configPath;
            return entity;
        }

        public static IEntity GetReadOnlyEntity(string configPath) {
#if UNITY_EDITOR
            if (!deserializedCache.ContainsKey(configPath)) {
                UnityEngine.Debug.Log("Config not found " + configPath);
            }
#endif

            var cache = deserializedCache[configPath];
            return (IEntity)cache;
        }

        public static T GetInstanceFromConfig<T>(string configPath) {
            if (deserializedCache.TryGetValue(configPath, out var cache)) {
                return (T)cache;
            } else {
                throw new Exception();
            }
        }

        public static void AddConfig(string configPath, string yamlContent) {
            yamlConfigs.Add(configPath, yamlContent);

            TryToCacheAutoType(configPath, yamlContent);
        }

        public static string SerializeToYaml(object instance) {
            return yamlSerializer.Serialize(instance);
        }

        public static T GetConfigTemplate<T>(string configPath) where T : Template {
            return (T)deserializedTemplates[configPath];
        }

        private static IEntity CreateEntity(Template template) {
            var entity = new Entity(0, NodesCollector.LastNodeTypeId + 1);
            var templateType = template.GetType();
            var componentsFields = templateType.GetProperties().Where(x => x.PropertyType.GetInterfaces().Any(i => i == iComponentType));

            foreach (var componentField in componentsFields) {
                var component = componentField.GetValue(template) as IComponent;
                if (component != null) {
                    entity.AddComponent(component);
                }
            }
            return entity;
        }

        private static T CreateInstanceFromContent<T>(string yamlContent) {
            return yamlDeserializer.Deserialize<T>(yamlContent);
        }

        private static object CreateInstanceFromContent(string yamlContent, Type type) {
            return yamlDeserializer.Deserialize(yamlContent, type);
        }

        private static void TryToCacheAutoType(string configPath, string yamlContent) {

            var targetTypeData = yamlDeserializer.Deserialize<ConfigData>(yamlContent);
            if (string.IsNullOrEmpty(targetTypeData.TargetTypeFullName)) return;

            var targetType = Type.GetType(targetTypeData.TargetTypeFullName);

            if (targetType == null) throw new TypeNotFoundException();

            var instance = CreateInstanceFromContent(yamlContent, targetType);

            if (basicTemplateType.IsAssignableFrom(targetType)) {
                var templateInstance = (Template)instance;
                var entity = CreateEntity(templateInstance);
                deserializedTemplates.Add(configPath, templateInstance);
                deserializedCache.Add(configPath, entity);
            } else {
                if (targetTypeData.IsSingleton) {
                    var instanceStaticField = targetType.GetField("INSTANCE", BindingFlags.Static | BindingFlags.Public);
                    instanceStaticField.SetValue(null, instance);
                }
                deserializedCache.Add(configPath, instance);
            }
        }

        private static string GetConfigYaml(string configPath) {
            yamlConfigs.TryGetValue(configPath, out var result);
            return result;
        }
    }
}
