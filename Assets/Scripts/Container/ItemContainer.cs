using UnityEngine;
using System.Collections;
using DeRibura;
using System.Collections.Generic;
using System;

public class ItemContainer : AbstractSingleTargetSelector<string, ItemUI>
{
    public List<ItemTuple> initialItems;
    public HashSet<string> UnavailableItems = new HashSet<string>();


    private void Awake()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        RegisterInitialItems(initialItems);
        OnClick += item => SelectIfAvailable(item);
    }

    public void SelectIfAvailable(ItemUI item)
    {
        var itemId = GetKey(item);
        if (UnavailableItems.Contains(itemId)) return;

        SelectItem(item);
    }

    public void MarkItemAsUnavailable(string key, bool isUnabailavle)
    {
        var item = GetItem(key);
        if (isUnabailavle)
        {
            UnavailableItems.Add(key);
            item.SetAvailable(isUnabailavle);
        } else
        {
            UnavailableItems.Remove(key);
            item.SetAvailable(isUnabailavle);
        }
    }

    public void InitIfNeed()
    {
        if (!isInited)
        {
            Init();
        }
    }

    public void AddItems(List<string> itemIds)
    {
        foreach (var itemId in itemIds)
        {
            var item = AddItem(itemId);
            item.Init(itemId);
        }
    }

    [Serializable]
    public class ItemTuple : SerializableTuple<string, ItemUI> { }
}
