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
    public ControllerWindows windowsController;


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
            if (pendantGem.gameObject.activeSelf)
            {
                pendantGem.Break();
            }
            gemGlue.gameObject.SetActive(false);
            timerSlider.gameObject.SetActive(false);
        };
        glueController.OnGlueUsed += pos => UseGemGlueIfPossible(pos);
        rightPendant.OnBreakEnd += () =>
        {
            rightPendant.gameObject.SetActive(false);
            selectors.ActiveItemContainer.MarkItemAsUnavailable("pendant-right", false);
        };
        pendantGem.OnBreakEnd += () =>
        {
            pendantGem.gameObject.SetActive(false);
            selectors.ActiveItemContainer.MarkItemAsUnavailable("pendant-gem", false);
        };
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
        if (gemArea.OverlapPoint(pos) && leftPendant.gameObject.activeSelf && rightPendant.gameObject.activeSelf
            && !rightPendant.IsBreaking)
        {
            gemGlue.gameObject.SetActive(true);
            LeftAndRightPartsTimer.Dur += 3;
            LeftAndRightPartsTimer.MaxDur = Mathf.Max(LeftAndRightPartsTimer.MaxDur, LeftAndRightPartsTimer.Dur);
        }
    }

    private void AddLockPendant(Vector2 pos)
    {
        if (leftPendant.gameObject.activeSelf && lockArea.OverlapPoint(pos))
        {
            pendantLock.gameObject.SetActive(true);
            GetComponent<AudioSource>().Play();
            selectors.ActiveItemContainer.FullDeselect();
            selectors.ActiveItemContainer.MarkItemAsUnavailable("pendant-lock", true);
            if (IsItemCompleted())
            {
                FinishLevel();
            }
        }
    }

    public void FinishLevel()
    {
        timerSlider.gameObject.SetActive(false);
        LeftAndRightPartsTimer.IsTimerActive = false;
        StartCoroutine(ShowSuccessDialog());
    }

    private IEnumerator ShowSuccessDialog()
    {
        yield return new WaitForSeconds(1);
        windowsController.ItemReady(0);
    }

    private bool IsItemCompleted()
    {

        return leftPendant.gameObject.activeSelf && rightPendant.gameObject.activeSelf
            && pendantLock.gameObject.activeSelf && pendantGem.gameObject.activeSelf
            && !rightPendant.IsBreaking && !pendantGem.IsBreaking;
    }

    private void AddGemPendant(Vector2 pos)
    {
        if (!gemArea.OverlapPoint(pos)) return;

        pendantGem.gameObject.SetActive(true);
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().Play();
        selectors.ActiveItemContainer.FullDeselect();
        selectors.ActiveItemContainer.MarkItemAsUnavailable("pendant-gem", true);
        if (!gemGlue.gameObject.activeSelf || rightPendant.IsBreaking)
        {
            pendantGem.Break();
        }
        else
        {
            if (IsItemCompleted())
            {
                FinishLevel();
            }
        }
    }

    private void AddRightPendant(Vector2 pos)
    {
        rightPendant.gameObject.SetActive(true);
        GetComponent<AudioSource>().Play();
        selectors.ActiveItemContainer.FullDeselect();
        selectors.ActiveItemContainer.MarkItemAsUnavailable("pendant-right", true);
        if (leftPendant.gameObject.activeSelf)
        {
            LeftAndRightPartsTimer.StartTimer();
            timerSlider.gameObject.SetActive(true);
        }
    }

    private void AddLeftPendant(Vector2 pos)
    {
        leftPendant.gameObject.SetActive(true);
        GetComponent<AudioSource>().Play();
        selectors.ActiveItemContainer.FullDeselect();
        selectors.ActiveItemContainer.MarkItemAsUnavailable("pendant-left", true);
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
