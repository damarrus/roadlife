﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControllerGrue : MonoBehaviour
{
    private  SelectorsController controllerSelectedItem;
    public Slider slider;
    private bool canMouseRight, canDoNext;
    private bool canMouseLeft = true;
    public event Action<Vector2> OnGlueUsed;

    public void Start()
    {
        controllerSelectedItem = GetComponent<SelectorsController>();
    }
    public void Update()
    {
        if (controllerSelectedItem.ActiveToolsContainer.LastSelectedItem != null)
        {
            var itemId = controllerSelectedItem.ActiveToolsContainer.GetKey(controllerSelectedItem.ActiveToolsContainer.LastSelectedItem);
            if (!canDoNext)
            {
                if (itemId == "glue")
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
                    if (slider.value == slider.maxValue)
                    {
                        canDoNext = true;
                        slider.gameObject.SetActive(false) ; 
                        OnGlueUsed.Invoke(Input.mousePosition);
                    }
                    slider.value -= 0.09f;

                }
            }
        }
    }
}
