using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ControllerArballet : MonoBehaviour
{
    public event Action<Quaternion> OnLeverUsed;
    private bool canMove;
    private float maxAngle = 1080;
    private float temp;
    private float angle, sumAngle;
    public Slider slider;

    private int count;
    private SelectorsController controllerSelectedItem;
    public event Action<Vector2> OnShowerUsed = delegate { };
    public void Start()
    {
        controllerSelectedItem = GetComponent<SelectorsController>();

    }
    public void Update()
    {
        if (controllerSelectedItem.ActiveToolsContainer.LastSelectedItem != null)
        {
            var itemId = controllerSelectedItem.ActiveToolsContainer.GetKey(controllerSelectedItem.ActiveToolsContainer.LastSelectedItem);
            if (itemId == "shower")
            {
                if (Input.mousePosition.x > temp)
                {
                    slider.value += 0.65f;
                }
                else if (Input.mousePosition.x < temp)
                {
                    slider.value += 0.65f;
                }
                if (slider.value == slider.maxValue)
                {
                    slider.gameObject.SetActive(false);
                    controllerSelectedItem.ActiveToolsContainer.FullDeselect();
                    OnShowerUsed.Invoke(Input.mousePosition);
                    slider.value = 0;
                }
                slider.value -= 0.5f;

            }
        }
    }

}
