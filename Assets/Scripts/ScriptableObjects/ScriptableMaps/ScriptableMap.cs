using DeRibura;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects {
    public abstract class ScriptableMap<K, V> : ScriptableObject {
        [SerializeField] private V fallback;
        [SerializeField] protected abstract IEnumerable<SerializableTuple<K, V>> items { get; }

        public V Get(K key) {
            foreach (var item in items) {
                if (item.Key.Equals(key)) {
                    return item.Value;
                }
            }
            return fallback;
        }
    }

    [Serializable]
    public class SerializableTuple<K, V>
    {
        public K Key;
        public V Value;
    }
}
