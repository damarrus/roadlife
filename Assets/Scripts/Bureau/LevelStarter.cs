using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelStarter : MonoBehaviour
{
    public PendantItemController pendantItemController;
    public SelectorsController selectorsController;
    public GameObject firstLevelItems;
    public ItemContainer firstLevelSelectorItemSelector;
    public ItemContainer firstLevelToolSelector; 
    public GameObject secondLevelItems;
    public ItemContainer secondLevelSelectorItemSelector;
    public ItemContainer secondLevelToolSelector;
    public GameObject thirdLevelItems;
    public ItemContainer thirdLevelSelectorItemSelector;
    public ItemContainer thirdLevelToolSelector;

    private void Start()
    {
        StartFirstLevel();
    }

    public void StartFirstLevel()
    {
        firstLevelItems.SetActive(true);
        selectorsController.SetSelectors(firstLevelSelectorItemSelector, firstLevelToolSelector);
    }

    public void StartSecondLevel()
    {
        secondLevelItems.SetActive(true);
        selectorsController.SetSelectors(secondLevelSelectorItemSelector, secondLevelToolSelector);
    }

    public void StartThirdLevel()
    {
        thirdLevelItems.SetActive(true);
        selectorsController.SetSelectors(thirdLevelSelectorItemSelector, thirdLevelToolSelector);
    }
}
