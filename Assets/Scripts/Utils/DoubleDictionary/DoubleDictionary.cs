using System;
using System.Collections.Generic;
using System.Linq;

namespace DeRibura {
    public class DoubleDictionary<K, V> {

        private Dictionary<K, V> kvps = new Dictionary<K, V>();
        private Dictionary<V, K> vkps = new Dictionary<V, K>();

        public void AddItem(K key, V value) {
            kvps.Add(key, value);
            vkps.Add(value, key);
        }

        public void RemoveByKey(K key) {
            var value = kvps[key];
            kvps.Remove(key);
            vkps.Remove(value);
        }

        public void RemoveByValue(V value) {
            var key = vkps[value];
            kvps.Remove(key);
            vkps.Remove(value);
        }

        public int Count() {
            return kvps.Count;
        }

        public V GetValue(K key) {
            return kvps[key];
        }

        public K GetKey(V value) {
            return vkps[value];
        }

        public bool HasKey(K key) {
            return kvps.ContainsKey(key);
        }

        public bool HasValue(V value) {
            return vkps.ContainsKey(value);
        }

        public IEnumerable<KeyValuePair<K, V>> GetItems(Predicate<V> predicate) {
            return kvps.Where(x => predicate(x.Value));
        }

        public Dictionary<K, V> GetItems() {
            return kvps;
        }

        public void Clear() {
            kvps.Clear();
            vkps.Clear();
        }
    }
}
