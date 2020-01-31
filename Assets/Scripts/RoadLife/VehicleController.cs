using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public CollisionEventor firstWheel;
    public CollisionEventor secondWheel;
    public Suspension Suspension;
    public Engine Engine;

    public float Speed;
    public float MaxSpeed;

    private void Start()
    {
        firstWheel.OnCollide += _ => Debug.Log("1");
        secondWheel.OnCollide += _ => Debug.Log("2");
    }
}
