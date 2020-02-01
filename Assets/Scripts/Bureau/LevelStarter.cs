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

    private void Start()
    {
        StartFirstLevel();
    }

    public void StartFirstLevel()
    {
        firstLevelItems.SetActive(true);
        selectorsController.SetSelectors(firstLevelSelectorItemSelector, firstLevelToolSelector);
    }

}
