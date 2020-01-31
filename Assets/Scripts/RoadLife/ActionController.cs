using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActionController : MonoBehaviour
{
    public DropPickUpController DropPickUpController;
    public VehicleController VehicleController;
    public float PlayerDistance;
    public float PlayTime;

    public Dictionary<float, Action> actionOnDistance;
    public Dictionary<float, Action> actionOnTime;

    public void Update()
    {
        PlayerDistance += VehicleController.Speed * Time.deltaTime;
        PlayTime += Time.deltaTime;


    }
}