using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class BoxItemController : MonoBehaviour
{
    public ControllerHammer hammerController;
    public SelectorsController selectors;
    public TableItemUI bottomBox;
    public TableItemUI cardKeyBox;
    public TableItemUI cardEnergyBox;
    public TableItemUI cardDarknessBox;
    public TableItemUI cardOldmanBox;
    public TableItemUI wallFrontBox;
    public TableItemUI wallBackBox;
    public TableItemUI wallLeftBox;
    public TableItemUI wallRightBox;
    public TableItemUI coverBox;
    public TableItemUI lockBox;

    public TableClicker tableClicker;
    public ColliderThing wallFrontRightHammer;
    public ColliderThing wallFrontLeftHammer;
    public ColliderThing wallBackLeftHammer;
    public ColliderThing wallBackRightHammer;
    public Slider timerSlider;
    

    public BoxCollider2D wallFrontRightArea;
    public BoxCollider2D wallFrontLeftArea;
    public BoxCollider2D wallBackLeftArea;
    public BoxCollider2D wallBackRightArea;
    public BoxCollider2D cardArea;
    public BoxCollider2D wallArea;
    public BoxCollider2D coverArea;
    public BoxCollider2D lockArea;

    public Timer BoxTimer;

    Dictionary<string, Action<Vector2>> AddMethods;

    void Start()
    {
        AddMethods = new Dictionary<string, Action<Vector2>>()
        {
            { "box-bottom", AddBottomBox },
            { "box-card-key", AddCardKeyBox },
            { "box-card-energy", AddCardEnergyBox },
            { "box-card-darkness", AddCardDarknessBox },
            { "box-card-oldman", AddCardOldmanBox },
            { "box-wall-front", AddWallFrontBox },
            { "box-wall-back", AddWallBackBox },
            { "box-wall-left", AddWallLeftBox },
            { "box-wall-right", AddWallRightBox },
            { "box-cover", AddCoverBox },
            { "box-lock", AddLockBox }
            // { "box-key", AddKeyBox }
        };

        tableClicker.OnTableClick += pos => CheckTableClick(pos);
        BoxTimer.OnEnd += () =>
        {
            if (wallFrontBox.gameObject.activeSelf){wallFrontBox.Break();}
            if (wallBackBox.gameObject.activeSelf){wallBackBox.Break();}
            if (wallLeftBox.gameObject.activeSelf){wallLeftBox.Break();}
            if (wallRightBox.gameObject.activeSelf){wallRightBox.Break();}
            if (coverBox.gameObject.activeSelf){coverBox.Break();}
            if (lockBox.gameObject.activeSelf){lockBox.Break();}

            wallFrontBox.Break();
            wallBackBox.Break();
            wallLeftBox.Break();
            wallRightBox.Break();
            coverBox.Break();
            lockBox.Break();

            wallFrontRightHammer.gameObject.SetActive(false);
            wallFrontLeftHammer.gameObject.SetActive(false);
            wallBackLeftHammer.gameObject.SetActive(false);
            wallBackRightHammer.gameObject.SetActive(false);

            timerSlider.gameObject.SetActive(false);
        };

        hammerController.OnHammerUsed += pos => {
            
            // TODO проверка

            UseWallFrontRightHammerIfPossible(pos);
            UseWallFrontLeftHammerIfPossible(pos);
            UseWallBackLeftHammerIfPossible(pos);
            UseWallBackRightHammerIfPossible(pos);
        };
        
        wallFrontBox.OnBreakEnd += () => wallFrontBox.gameObject.SetActive(false);
        wallBackBox.OnBreakEnd += () => wallBackBox.gameObject.SetActive(false);
        wallLeftBox.OnBreakEnd += () => wallLeftBox.gameObject.SetActive(false);
        wallRightBox.OnBreakEnd += () => wallRightBox.gameObject.SetActive(false);
        coverBox.OnBreakEnd += () => coverBox.gameObject.SetActive(false);
        lockBox.OnBreakEnd += () => lockBox.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (timerSlider.gameObject.activeSelf)
        {
            timerSlider.value = BoxTimer.Dur / BoxTimer.MaxDur;
        }
    }

    private void UseWallFrontRightHammerIfPossible(Vector2 pos)
    {
        if (wallFrontRightArea.OverlapPoint(pos) && !wallFrontRightHammer.gameObject.activeSelf) 
        {
            wallFrontRightHammer.gameObject.SetActive(true);
            BoxTimer.Dur += 5;
            BoxTimer.MaxDur = Mathf.Max(BoxTimer.MaxDur, BoxTimer.Dur);
        }
    }

    private void UseWallFrontLeftHammerIfPossible(Vector2 pos)
    {
        if (wallFrontLeftArea.OverlapPoint(pos) && !wallFrontLeftHammer.gameObject.activeSelf) 
        {
            wallFrontLeftHammer.gameObject.SetActive(true);
            BoxTimer.Dur += 5;
            BoxTimer.MaxDur = Mathf.Max(BoxTimer.MaxDur, BoxTimer.Dur);
        }
    }

    private void UseWallBackLeftHammerIfPossible(Vector2 pos)
    {
        if (wallBackLeftArea.OverlapPoint(pos) && !wallBackLeftHammer.gameObject.activeSelf) 
        {
            wallBackLeftHammer.gameObject.SetActive(true);
            BoxTimer.Dur += 5;
            BoxTimer.MaxDur = Mathf.Max(BoxTimer.MaxDur, BoxTimer.Dur);
        }
    }

    private void UseWallBackRightHammerIfPossible(Vector2 pos)
    {
        if (wallBackRightArea.OverlapPoint(pos) && !wallBackRightHammer.gameObject.activeSelf) 
        {
            wallBackRightHammer.gameObject.SetActive(true);
            BoxTimer.Dur += 5;
            BoxTimer.MaxDur = Mathf.Max(BoxTimer.MaxDur, BoxTimer.Dur);
        }
    }

    private void AddBottomBox(Vector2 pos)
    {
        bottomBox.gameObject.SetActive(true);
        selectors.ActiveItemContainer.FullDeselect();
        selectors.ActiveItemContainer.MarkItemAsUnavailable("box-bottom", true);
    }

    private void AddCardKeyBox(Vector2 pos) {AddCardBox(pos, "key");}
    private void AddCardEnergyBox(Vector2 pos) {AddCardBox(pos, "energy");}
    private void AddCardDarknessBox(Vector2 pos) {AddCardBox(pos, "darkness");}
    private void AddCardOldmanBox(Vector2 pos) {AddCardBox(pos, "oldman");}

    private void AddCardBox(Vector2 pos, string cardType)
    {
        if (!cardArea.OverlapPoint(pos)) return;
        // Нужно не только курсор очистить, но и удалить сам объект карты
        selectors.ActiveItemContainer.FullDeselect();

        // Проверка на то, что дно поставили на сцену
        if (bottomBox.gameObject.activeSelf) 
        {
            if (cardType == "key") {
                // Выдвигается дно с ключом, ключ летит в инвентарь, карта исчезает
            } else if (cardType == "energy") {
                // Выдвигается дно с энергией, энергия летит в инвентарь, карта исчезает
            } else if (cardType == "darkness") {
                // Все части на поле убираются в инвентарь, карта исчезает
            } else {
                // Ничего не происходит, карта исчезает
            }
        }
    }

    private void AddWallFrontBox(Vector2 pos) {AddWallBox(pos, "front");}
    private void AddWallBackBox(Vector2 pos) {AddWallBox(pos, "back");}
    private void AddWallLeftBox(Vector2 pos) {AddWallBox(pos, "left");}
    private void AddWallRightBox(Vector2 pos) {AddWallBox(pos, "right");}

    private void AddWallBox(Vector2 pos, string wallType)
    {
        if (!wallArea.OverlapPoint(pos)) return;

        selectors.ActiveItemContainer.FullDeselect();

        // Проверка на то, что дно поставили на сцену
        if (bottomBox.gameObject.activeSelf) 
        {
            if (wallType == "front") {
                wallFrontBox.gameObject.SetActive(true);
            } else if (wallType == "back") {
                wallBackBox.gameObject.SetActive(true);
            } else if (wallType == "left") {
                wallLeftBox.gameObject.SetActive(true);
            } else if (wallType == "right") {
                wallRightBox.gameObject.SetActive(true);
            } else {
                return;
            }

            if (!timerSlider.gameObject.activeSelf) 
            {
                BoxTimer.StartTimer();
                timerSlider.gameObject.SetActive(true);
            }

            BoxTimer.Dur += 5;
            BoxTimer.MaxDur = Mathf.Max(BoxTimer.MaxDur, BoxTimer.Dur);

        }
    }

    private void AddCoverBox(Vector2 pos)
    {
        if (!coverArea.OverlapPoint(pos)) return;

        if (wallFrontBox.gameObject.activeSelf &&
            wallBackBox.gameObject.activeSelf &&
            wallLeftBox.gameObject.activeSelf &&
            wallRightBox.gameObject.activeSelf) 
        {
            selectors.ActiveItemContainer.FullDeselect();
            coverBox.gameObject.SetActive(true);
        }
    }

    private void AddLockBox(Vector2 pos)
    {
        if (!coverArea.OverlapPoint(pos)) return;

        if (wallFrontBox.gameObject.activeSelf &&
            wallBackBox.gameObject.activeSelf &&
            wallLeftBox.gameObject.activeSelf &&
            wallRightBox.gameObject.activeSelf) 
        {
            selectors.ActiveItemContainer.FullDeselect();
            coverBox.gameObject.SetActive(true);
        }
    }

    // private void AddGemPendant(Vector2 pos)
    // {
    //     if (!gemArea.OverlapPoint(pos)) return;

    //     pendantGem.gameObject.SetActive(true);
    //     selectors.ActiveItemContainer.FullDeselect();
    //     if (!gemGlue.gameObject.activeSelf)
    //     {
    //         pendantGem.Break();
    //     }
    // }

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
