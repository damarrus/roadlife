using DeRibura;
using ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, ISelectableItem<ItemUI>
{
    public Button button;
    public Image image;
    public ScriptableImageMap imageMap;
    public GameObject Selector;
    public GameObject UnavailableMask;


    public event Action<ItemUI> OnClick = delegate { };

    void Awake()
    {
        button.onClick.AddListener(() => OnClick.Invoke(this));
    }

    public void SetSelection(bool isSelected)
    {
        Selector.SetActive(isSelected);
    }

    public void Init(string id)
    {
        image.sprite = imageMap.Get(id);
    }

    internal void SetAvailable(bool isAvailable)
    {
        UnavailableMask.SetActive(!isAvailable);
    }
}
