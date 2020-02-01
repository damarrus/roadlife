using System;
using System.Linq;
using UnityEngine;

namespace DeRibura
{
    public abstract class AbstractSingleTargetSelector<K, T> : AbstractContainer<K, T> where T : MonoBehaviour, ISelectableItem<T>
    {
        
        [SerializeField] protected bool IsDeselectable;

        public event Action<T> OnClick = delegate { };
        public event Action<T> OnSelect = delegate { };
        public event Action<T> OnSelectSame = delegate { };
        public event DeselectionHandler<T> OnDeselect = delegate { };

        public T LastSelectedItem { get; protected set; }

        public void SelectItemByKey(K key)
        {
            var itemToSelect = items.GetValue(key);
            SelectOrDeselectItem(itemToSelect);
        }

        public virtual void SelectOrDeselectItem(T item)
        {
            if (IsDeselectable && (item == LastSelectedItem || item == null))
            {
                FullDeselect();
            }
            else
            {
                SelectItem(item);
            }
        }

        public virtual void FullDeselect()
        {
            Deselect(true);
        }

        private void Deselect(bool isFullDeselect)
        {
            LastSelectedItem?.SetSelection(false);
            if (LastSelectedItem != null)
            {
                OnDeselect(LastSelectedItem, isFullDeselect);
                LastSelectedItem = null;
            }
        }

        public virtual void SelectItem(T item)
        {
            if (item == null) return;

            var isSelectSame = LastSelectedItem == item;
            Deselect(false);
            if (isSelectSame)
            {
                OnSelectSame(item);
            }
            else
            {
                OnSelect(item);
            }
            if (!IsDeselectable || !isSelectSame)
            {
                LastSelectedItem = item;
                LastSelectedItem.SetSelection(true);
            }
        }

        protected override void RegisterItem(K key, T item)
        {
            base.RegisterItem(key, item);
            item.OnClick += selectedItem => OnClick(selectedItem);
        }

        public bool TryToSelect(Predicate<T> predicate)
        {
            var itemToSelect = GetItems(predicate).FirstOrDefault().Value;
            if (itemToSelect != null)
            {
                SelectOrDeselectItem(itemToSelect);
            }
            return itemToSelect != null;
        }

        public bool TryToSelectByKey(K key)
        {
            var isExist = items.HasKey(key);
            if (items.HasKey(key))
            {
                SelectItemByKey(key);
            }
            return isExist;
        }

        void OnValidate()
        {
            if (prefab != null && !(prefab is ISelectableItem<T>))
            {
                Debug.LogError("Prefab must implement 'ISelectableItem' interface");
                prefab = null;
            }
        }

        public delegate void DeselectionHandler<TT>(TT item, bool isFullDeselect);
    }
}
