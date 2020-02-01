using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class PendantItemController : MonoBehaviour
{
    public ControllerGrue glueController;
    public SelectorsController selectors;
    public TableItemUI leftPendant;
    public TableItemUI rightPendant;
    public TableItemUI pendantGem;
    public TableItemUI pendantLock;
    public TableClicker tableClicker;
    public ColliderThing gemGlue;
    public Slider timerSlider;
    

    public BoxCollider2D gemArea;
    public BoxCollider2D lockArea;

    public Timer LeftAndRightPartsTimer;

    Dictionary<string, Action<Vector2>> AddMethods;

    void Start()
    {
        AddMethods = new Dictionary<string, Action<Vector2>>()
        {
            { "pendant-left", AddLeftPendant },
            { "pendant-right", AddRightPendant },
            { "pendant-gem", AddGemPendant },
            { "pendant-lock", AddLockPendant }
        };

        tableClicker.OnTableClick += pos => CheckTableClick(pos);
        LeftAndRightPartsTimer.OnEnd += () =>
        {
            rightPendant.Break();
            pendantGem.Break();
            gemGlue.gameObject.SetActive(false);
            timerSlider.gameObject.SetActive(false);
        };
        glueController.OnGlueUsed += pos => UseGemGlueIfPossible(pos);
        rightPendant.OnBreakEnd += () => rightPendant.gameObject.SetActive(false);
        pendantGem.OnBreakEnd += () => pendantGem.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (timerSlider.gameObject.activeSelf)
        {
            timerSlider.value = LeftAndRightPartsTimer.Dur / LeftAndRightPartsTimer.MaxDur;
        }
    }

    private void UseGemGlueIfPossible(Vector2 pos)
    {
        if (gemArea.OverlapPoint(pos))
        {
            gemGlue.gameObject.SetActive(true);
            LeftAndRightPartsTimer.Dur += 5;
            LeftAndRightPartsTimer.MaxDur = Mathf.Max(LeftAndRightPartsTimer.MaxDur, LeftAndRightPartsTimer.Dur);
        }
    }

    private void AddLockPendant(Vector2 pos)
    {
    }

    private void AddGemPendant(Vector2 pos)
    {
        if (!gemArea.OverlapPoint(pos)) return;

        pendantGem.gameObject.SetActive(true);
        selectors.ActiveItemContainer.FullDeselect();
        if (!gemGlue.gameObject.activeSelf)
        {
            pendantGem.Break();
        }
    }

    private void AddRightPendant(Vector2 pos)
    {
        rightPendant.gameObject.SetActive(true);
        selectors.ActiveItemContainer.FullDeselect();
        if (leftPendant.gameObject.activeSelf)
        {
            LeftAndRightPartsTimer.StartTimer();
            timerSlider.gameObject.SetActive(true);
        }
    }

    private void AddLeftPendant(Vector2 pos)
    {
        leftPendant.gameObject.SetActive(true);
        selectors.ActiveItemContainer.FullDeselect();
        if (rightPendant.gameObject.activeSelf)
        {
            LeftAndRightPartsTimer.StartTimer();
            timerSlider.gameObject.SetActive(true);
        }
    }

    private void CheckTableClick(Vector2 pos)
    {
        if (selectors.ActiveItemContainer.LastSelectedItem != null)
        {
            var mouseItem = selectors.ActiveItemContainer.LastSelectedItem;
            var itemId = selectors.ActiveItemContainer.GetKey(selectors.ActiveItemContainer.LastSelectedItem);

            AddMethods[itemId].Invoke(pos);
        }
    }
}
