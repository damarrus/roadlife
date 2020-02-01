using System;
using UnityEngine;

namespace DeRibura
{
    public interface ISelectableItem<T> where T : MonoBehaviour
    {
        event Action<T> OnClick;
        void SetSelection(bool isSelected);
    }
}