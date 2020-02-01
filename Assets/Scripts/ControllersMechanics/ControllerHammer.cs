using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ControllerHammer : MonoBehaviour
{

    private SelectorsController controllerSelectedItem;
    public Slider slider;
    private bool canMouseRight;
    private bool canMouseLeft = true;
    public event Action<Vector2> OnHammerUsed = delegate { };

    public void Start()
    {
        controllerSelectedItem = GetComponent<SelectorsController>();
    }
    public void Update()
    {
        if (controllerSelectedItem.ActiveToolsContainer.LastSelectedItem != null)
        {
            var itemId = controllerSelectedItem.ActiveToolsContainer.GetKey(controllerSelectedItem.ActiveToolsContainer.LastSelectedItem);
            if (itemId == "hammer")
            {
                    if (Input.GetMouseButtonDown(0))
                    {
                        slider.value += 1;
                    }
            
                if (slider.value == slider.maxValue)
                {
                    slider.gameObject.SetActive(false);
                    controllerSelectedItem.ActiveToolsContainer.FullDeselect();
                    OnHammerUsed.Invoke(Input.mousePosition);
                    slider.value = 0;
                }
                slider.value -= 0.05f;
            }

        }
    }
}
