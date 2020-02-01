using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace DeRibura
{
    public abstract class AbstractContainer<K, T> : MonoBehaviour where T : MonoBehaviour
    {

        [SerializeField] protected T prefab;
        [SerializeField] protected RectTransform container;
        [SerializeField] protected int initialPoolCount;

        protected EnlargablePool<T> pool;
        protected DoubleDictionary<K, T> items = new DoubleDictionary<K, T>();
        protected bool isInited;

        public virtual void Init()
        {
            pool = new EnlargablePool<T>(InstantiateItem, initialPoolCount);
            isInited = true;
        }

        protected T InstantiateItem()
        {
            return Instantiate(prefab, container);
        }

        public T AddItem(K key)
        {
            var item = pool.GetItem();
            RegisterItem(key, item);
            return item;
        }

        public void RemoveItemByKey(K key)
        {
            var itemToRemove = items.GetValue(key);
            items.RemoveByKey(key);
            pool.Return(itemToRemove);
        }

        public void RemoveItem(T item)
        {
            items.RemoveByValue(item);
            pool.Return(item);
        }

        public T GetItem(K key)
        {
            return items.GetValue(key);
        }


        public K GetKey(T item)
        {
            return items.GetKey(item);
        }
        protected virtual void RegisterItem(K key, T item)
        {
            items.AddItem(key, item);
        }

        public void RegisterInitialItems(IEnumerable<SerializableTuple<K, T>> initialElements)
        {
            foreach (var item in initialElements)
            {
                pool.Register(item.Value);
                if (item.Value.gameObject.activeSelf)
                {
                    RegisterItem(item.Key, item.Value);
                }
            }
        }

        public void MarkForReuse(int count)
        {
            pool.MarkForReuse(count);
        }

        public List<KeyValuePair<K, T>> GetItems(Predicate<T> predicate)
        {
            return items.GetItems(predicate).ToList();
        }

        public void Clear()
        {
            pool.ReturnAll();
            items.Clear();
        }
    }

    [Serializable]
    public abstract class SerializableTuple<K, V>
    {
        public K Key;
        public V Value;
    }
}
