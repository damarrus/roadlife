//using UnityEngine;
//using System.Collections;
//using System;
//using System.Collections.Generic;
//using UnityEngine.UI;

//public class BoxItemController : MonoBehaviour
//{
//    public ControllerGrue glueController;
//    public SelectorsController selectors;
//    public TableItemUI bottomBox;
//    public TableItemUI cardKeyBox;
//    public TableItemUI cardEnergyBox;
//    public TableItemUI cardDarknessBox;
//    public TableItemUI cardOldmanBox;
//    public TableItemUI boltBox;
//    public TableItemUI wallFrontBox;
//    public TableItemUI wallBackBox;
//    public TableItemUI wallLeftBox;
//    public TableItemUI wallRightBox;
//    public TableItemUI coverBox;
//    public TableItemUI lockBox;

//    public TableClicker tableClicker;
//    public ColliderThing gemGlue;
//    public Slider timerSlider;
    

//    public BoxCollider2D gemArea;
//    public BoxCollider2D lockArea;

//    public Timer BoxTimer;

//    Dictionary<string, Action<Vector2>> AddMethods;

//    void Start()
//    {
//        AddMethods = new Dictionary<string, Action<Vector2>>()
//        {
//            { "box-bottom", AddBottomBox },
//            { "box-card-key", AddCardKeyBox },
//            { "box-card-energy", AddCardEnergyBox },
//            { "box-card-darkness", AddCardDarknessBox },
//            { "box-card-oldman", AddCardOldmanBox },
//            { "box-bolt", AddBoltBox },
//            { "box-wall-front", AddWallFrontBox },
//            { "box-wall-back", AddWallBackBox },
//            { "box-wall-left", AddWallLeftBox },
//            { "box-wall-right", AddWallRightBox },
//            { "box-cover", AddCoverBox },
//            { "box-lock", AddLockBox },
//            { "box-key", AddKeyBox },
//            { "box-key", AddBox }
//        };

//        tableClicker.OnTableClick += pos => CheckTableClick(pos);
//        BoxTimer.OnEnd += () =>
//        {
//            wallFrontBox.Break();
//            wallBackBox.Break();
//            wallLeftBox.Break();
//            wallRightBox.Break();
//            gemGlue.gameObject.SetActive(false);
//            timerSlider.gameObject.SetActive(false);
//        };
//        glueController.OnGlueUsed += pos => UseGemGlueIfPossible(pos);
//        rightPendant.OnBreakEnd += () => rightPendant.gameObject.SetActive(false);
//        pendantGem.OnBreakEnd += () => pendantGem.gameObject.SetActive(false);
//    }

//    private void Update()
//    {
//        if (timerSlider.gameObject.activeSelf)
//        {
//            timerSlider.value = BoxTimer.Dur / BoxTimer.MaxDur;
//        }
//    }

//    private void UseGemGlueIfPossible(Vector2 pos)
//    {
//        if (gemArea.OverlapPoint(pos))
//        {
//            gemGlue.gameObject.SetActive(true);
//            BoxTimer.Dur += 5;
//            BoxTimer.MaxDur = Mathf.Max(BoxTimer.MaxDur, BoxTimer.Dur);
//        }
//    }

//    private void AddBottomBox(Vector2 pos)
//    {
//        bottomBox.gameObject.SetActive(true);
//    }

//    private void AddCardKeyBox(Vector2 pos) {AddCardBox(pos, "key")}
//    private void AddCardEnergyBox(Vector2 pos) {AddCardBox(pos, "energy")}
//    private void AddCardDarknessBox(Vector2 pos) {AddCardBox(pos, "darkness")}
//    private void AddCardOldmanBox(Vector2 pos) {AddCardBox(pos, "oldman")}

//    private void AddCardBox(Vector2 pos, string cardType)
//    {
//        // Нужно не только курсор очистить, но и удалить сам объект карты
//        selectors.ActiveItemContainer.FullDeselect();

//        // Проверка на то, что дно поставили на сцену
//        if (bottomBox.gameObject.activeSelf) 
//        {
//            if (cardType == "key") {
//                // Выдвигается дно с ключом, ключ летит в инвентарь, карта исчезает
//            } else if (cardType == "energy") {
//                // Выдвигается дно с энергией, энергия летит в инвентарь, карта исчезает
//            } else if (cardType == "darkness") {
//                // Все части на поле убираются в инвентарь, карта исчезает
//            } else {
//                // Ничего не происходит, карта исчезает
//            }
//        }
//    }

//    private void AddWallFrontBox(Vector2 pos) {AddWallBox(pos, "front")}
//    private void AddWallBackBox(Vector2 pos) {AddWallBox(pos, "back")}
//    private void AddWallLeftBox(Vector2 pos) {AddWallBox(pos, "left")}
//    private void AddWallRightBox(Vector2 pos) {AddWallBox(pos, "right")}

//    private void AddWallBox(Vector2 pos, string wallType)
//    {
//        // Нужно не только курсор очистить, но и удалить сам объект карты
//        selectors.ActiveItemContainer.FullDeselect();

//        // Проверка на то, что дно поставили на сцену
//        if (bottomBox.gameObject.activeSelf) 
//        {
//            if (wallType == "front") {
//                wallFrontBox.gameObject.SetActive(true);
//            } else if (wallType == "back") {
//                wallBackBox.gameObject.SetActive(true);
//            } else if (wallType == "left") {
//                wallLeftBox.gameObject.SetActive(true);
//            } else if (wallType == "right") {
//                wallRightBox.gameObject.SetActive(true);
//            } else {
//                return;
//            }

//            if (!timerSlider.gameObject.activeSelf) 
//            {
//                BoxTimer.StartTimer();
//                timerSlider.gameObject.SetActive(true);
//            }

//            BoxTimer.Dur += 5;
//            BoxTimer.MaxDur = Mathf.Max(BoxTimer.MaxDur, BoxTimer.Dur);

//        }
//    }

//    private void AddCoverBox(Vector2 pos)
//    {
//        if (!wallArea.OverlapPoint(pos)) return;

//        if (wallFrontBox.gameObject.activeSelf &&
//            wallBackBox.gameObject.activeSelf &&
//            wallLeftBox.gameObject.activeSelf &&
//            wallRightBox.gameObject.activeSelf) 
//        {
//            selectors.ActiveItemContainer.FullDeselect();
//            coverBox.gameObject.SetActive(true);
//        }
//    }

//    // private void AddGemPendant(Vector2 pos)
//    // {
//    //     if (!gemArea.OverlapPoint(pos)) return;

//    //     pendantGem.gameObject.SetActive(true);
//    //     selectors.ActiveItemContainer.FullDeselect();
//    //     if (!gemGlue.gameObject.activeSelf)
//    //     {
//    //         pendantGem.Break();
//    //     }
//    // }

//    private void CheckTableClick(Vector2 pos)
//    {
//        if (selectors.ActiveItemContainer.LastSelectedItem != null)
//        {
//            var mouseItem = selectors.ActiveItemContainer.LastSelectedItem;
//            var itemId = selectors.ActiveItemContainer.GetKey(selectors.ActiveItemContainer.LastSelectedItem);

//            AddMethods[itemId].Invoke(pos);
//        }
//    }
//}
