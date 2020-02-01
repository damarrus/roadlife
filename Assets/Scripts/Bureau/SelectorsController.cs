using UnityEngine;
using System.Collections;
using System;

public class SelectorsController : MonoBehaviour
{
    public MouseItemUI MouseUI;

    [HideInInspector] public ItemContainer ActiveItemContainer;
    [HideInInspector] public ItemContainer ActiveToolsContainer;

    private void SubscribeToContainers()
    {
        ActiveItemContainer.OnSelect += item => DeselectToolAndSelectItem(item);
        ActiveToolsContainer.OnSelect += item => DeselectItemAndSelectTool(item);
        ActiveItemContainer.OnDeselect += (_, __) => MouseUI.gameObject.SetActive(false);
        ActiveToolsContainer.OnDeselect += (_, __) => MouseUI.gameObject.SetActive(false);
    }

    public void SetSelectors(ItemContainer itemContainer, ItemContainer toolsContainer)
    {
        ActiveItemContainer = itemContainer;
        ActiveToolsContainer = toolsContainer;
        SubscribeToContainers();
    }

    public void DeselectToolAndSelectItem(ItemUI item)
    {
        ActiveToolsContainer.FullDeselect();
        var id = ActiveItemContainer.GetKey(item);

        MouseUI.gameObject.SetActive(true);
        MouseUI.SetMouse(id, false);
    }

    public void DeselectItemAndSelectTool(ItemUI tool)
    {
        ActiveItemContainer.FullDeselect();
        var id = ActiveToolsContainer.GetKey(tool);
        MouseUI.gameObject.SetActive(true);
        MouseUI.SetMouse(id, true);
    }
}
