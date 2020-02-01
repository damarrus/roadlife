using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PendantItemController : MonoBehaviour
{
    public SelectorsController selectors;
    public TableItemUI leftPendant;
    public TableItemUI rightPendant;
    public TableItemUI pendantGem;
    public TableItemUI pendantLock;
    public TableClicker tableClicker;

    public Timer LeftAndRightPartsTimer;

    Dictionary<string, Action> AddMethods;

    void Start()
    {
        AddMethods = new Dictionary<string, Action>()
        {
            { "pendant-left", AddLeftPendant },
            { "pendant-right", AddRightPendant },
            { "pendant-gem", AddGemPendant },
            { "pendant-lock", AddLockPendant }
        };

        tableClicker.OnTableClick += pos => CheckTableClick(pos);
        LeftAndRightPartsTimer.OnEnd += () => rightPendant.Break();
    }

    private void AddLockPendant()
    {
        throw new NotImplementedException();
    }

    private void AddGemPendant()
    {
        throw new NotImplementedException();
    }

    private void AddRightPendant()
    {
        rightPendant.gameObject.SetActive(true);
        if (leftPendant.gameObject.activeSelf)
        {
            LeftAndRightPartsTimer.StartTimer();
        }
    }

    private void AddLeftPendant()
    {
        leftPendant.gameObject.SetActive(true);
        if (rightPendant.gameObject.activeSelf)
        {
            LeftAndRightPartsTimer.StartTimer();
        }

    }

    private void CheckLeftPendant()
    {
        //if ()
    }

    private void CheckTableClick(Vector2 pos)
    {
        if (selectors.ActiveItemContainer.LastSelectedItem != null)
        {
            var mouseItem = selectors.ActiveItemContainer.LastSelectedItem;
            var itemId = selectors.ActiveItemContainer.GetKey(selectors.ActiveItemContainer.LastSelectedItem);
            //Add
        }
    }

    private void AddGem()
    {
        //if 
    }
}
