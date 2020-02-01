using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControllerGrue : MonoBehaviour
{
    private  SelectorsController controllerSelectedItem;
    public Slider slider;
    private bool canMouseRight;
    private bool canMouseLeft = true;
    
    public void Start()
    {
        controllerSelectedItem = GetComponent<SelectorsController>();
    }
    public void Update()
    {
        var itemId = controllerSelectedItem.ActiveToolsContainer.GetKey(controllerSelectedItem.ActiveToolsContainer.LastSelectedItem);
        if(itemId == "Grue")
        {
            if (canMouseLeft == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    slider.value += 1;
                    canMouseLeft = false;
                    canMouseRight = true;
                }
            }
            else if (canMouseRight == true)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    slider.value += 1f;
                    canMouseRight = false;
                    canMouseLeft = true;
                }
            }
            slider.value -= 0.5f;
        }

       
    }
}
