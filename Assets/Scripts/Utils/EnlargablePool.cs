using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils {
    public class EnlargablePool<T> where T : MonoBehaviour {

        private readonly Func<T> instantiate;
        private Queue<T> freeObjects = new Queue<T>();
        private Queue<T> objectsForReuse = new Queue<T>();
        private HashSet<T> objectsInUse = new HashSet<T>();

        public EnlargablePool(Func<T> instantiate, int initialCount) {
            this.instantiate = instantiate;
            for (int i = 0; i < initialCount; i++) {
                var newItem = instantiate();
                newItem.gameObject.SetActive(false);
                freeObjects.Enqueue(newItem);
            }
        }

        public void ReturnAll() {
            foreach (var obj in objectsInUse) {
                freeObjects.Enqueue(obj);
                obj.gameObject.SetActive(false);
            }
            objectsInUse.Clear();
        }

        public T GetItem() {
            T result;
            if (objectsForReuse.Count > 0) {
                result = objectsForReuse.Dequeue();
            } else if (freeObjects.Count > 0) {
                result = freeObjects.Dequeue();
            } else {
                result = instantiate();
            }
            objectsInUse.Add(result);
            result.gameObject.SetActive(true);
            return result;
        }

        public void Return(T item) {
            objectsInUse.Remove(item);
            freeObjects.Enqueue(item);
            item.gameObject.SetActive(false);
        }

        public void Register(T item) {
            if (item.gameObject.activeSelf) {
                objectsInUse.Add(item);
            } else {
                freeObjects.Enqueue(item);
            }
        }

        public void MarkForReuse(int count) {
            count = Math.Min(objectsInUse.Count, count);

            var i = 0;
            while (objectsInUse.Count > 0 && i < count) {
                var itemToReuse = objectsInUse.First();
                objectsInUse.Remove(itemToReuse);
                objectsForReuse.Enqueue(itemToReuse);
                i++;
            }
        }

        public void Destroy() {
            foreach (var item in freeObjects) {
                GameObject.Destroy(item);
            }
            foreach (var item in objectsInUse) {
                GameObject.Destroy(item);
            }
            foreach (var item in objectsForReuse) {
                GameObject.Destroy(item);
            }
            freeObjects.Clear();
            objectsInUse.Clear();
            objectsForReuse.Clear();
        }
    }
}
