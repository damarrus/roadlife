using MadaoEcs;
using RaidHealer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class VisualEcs : EditorWindow
{
    private string componentsFilter = string.Empty;
    private Dictionary<ulong, bool> entitiesFoldout = new Dictionary<ulong, bool>();
    private Dictionary<IComponent, bool> componentsFoldout = new Dictionary<IComponent, bool>();
    private static Dictionary<ulong, IEntity> entities;
    private Vector2 scrollPos;

    private const string ENTITIES = "entities";

    [MenuItem("MadaoEcs/VisualEcs")]
    public static void ShowWindow() {
        GetWindow(typeof(VisualEcs));
        GetEntities();
    }

    void OnGUI() {
        if (!Application.isPlaying) return;

        if (entities == null) {
            GetEntities();
        }

        EditorGUILayout.LabelField("Filters", EditorStyles.boldLabel);
        componentsFilter = EditorGUILayout.TextField("Components", componentsFilter.ToLower());

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Entities", EditorStyles.boldLabel);

        var componentsToDraw = componentsFilter.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        var filteredEntities = GetFilteredEntities(componentsToDraw);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height - 100));

        foreach (var entityKvp in filteredEntities) {
            var id = entityKvp.Key;
            var entity = entityKvp.Value;

            if (!entitiesFoldout.TryGetValue(id, out var isEntityOpen)) {
                entitiesFoldout.Add(id, false);
            }
            DrawEntity(id, entity, isEntityOpen, componentsToDraw);
        }

        EditorGUILayout.EndScrollView();
    }

    private static void GetEntities() {
        entities = (Dictionary<ulong, IEntity>)typeof(Ecs).GetField(ENTITIES, BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
    }

    private Dictionary<ulong, IEntity> GetFilteredEntities(List<string> componentsToDraw) {
        if (!componentsToDraw.Any()) return entities;

        return entities.Where(x => componentsToDraw.All(y => x.Value.GetAllComponents().Any(z => z.GetType().Name.ToLower().Contains(y)))).ToDictionary(x => x.Key, x => x.Value);
    }

    private void DrawEntity(ulong id, IEntity entity, bool isEntityOpen, List<string> componentsToDraw) {
        entitiesFoldout[id] = EditorGUILayout.Foldout(isEntityOpen, $"{id}: {entity.Name ?? entity.ConfigPath}");
        if (entitiesFoldout[id]) {
            EditorGUI.indentLevel++;

            foreach (var component in entity.GetAllComponents()) {
                if (!componentsToDraw.Any() || componentsToDraw.Any(x => component.GetType().Name.ToLower().Contains(x))) {
                    DrawComponent(component);
                }
            }

            EditorGUI.indentLevel--;
        }
    }

    private void DrawComponent(IComponent component) {
        if (!componentsFoldout.TryGetValue(component, out var isComponentOpen)) {
            componentsFoldout.Add(component, false);
        }

        var componentType = component.GetType();
        componentsFoldout[component] = EditorGUILayout.Foldout(isComponentOpen, componentType.Name);
        if (componentsFoldout[component]) {

            EditorGUI.indentLevel++;
            if (typeof(MonoBehaviour).IsAssignableFrom(componentType) && false) {
                EditorGUILayout.LabelField($"MonoBehTrash");
            } else {
                foreach (var property in componentType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)) {
                    DrawField(component, property);
                }
                foreach (var field in componentType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)) {
                    DrawField(component, field);
                }
                foreach (var field in componentType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.GetCustomAttribute<Visual>() != null)) {
                    DrawField(component, field);
                }
            }

            EditorGUI.indentLevel--;
        }
    }

    private static void DrawField(IComponent component, PropertyInfo propertyInfo) {
        EditorGUI.indentLevel++;
        if (propertyInfo.PropertyType != typeof(string) && propertyInfo.PropertyType.GetInterfaces().Any(x => x == typeof(IEnumerable))) {
            var collection = propertyInfo.GetValue(component) as IEnumerable;
            DrawFieldAsCollection(component, collection, propertyInfo.Name);
        } else {
            EditorGUILayout.LabelField($"{propertyInfo.Name}: {propertyInfo.GetValue(component)}");
        }
        EditorGUI.indentLevel--;
    }

    private static void DrawField(IComponent component, FieldInfo fieldInfo) {
        EditorGUI.indentLevel++;
        if (fieldInfo.FieldType != typeof(string) && fieldInfo.FieldType.GetInterfaces().Any(x => x == typeof(IEnumerable))) {
            var collection = fieldInfo.GetValue(component) as IEnumerable;
            DrawFieldAsCollection(component, collection, fieldInfo.Name);
        } else {
            EditorGUILayout.LabelField($"{fieldInfo.Name}: {fieldInfo.GetValue(component)}");
        }
        EditorGUI.indentLevel--;
    }

    private static void DrawFieldAsCollection(IComponent component, IEnumerable collection, string name) {
        EditorGUILayout.LabelField(name + ":");
        if (collection == null) {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("null collection");
            EditorGUI.indentLevel--;
        } else {
            foreach (var item in collection) {
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField(item.ToString());
                EditorGUI.indentLevel--;
            }
        }
    }
}
