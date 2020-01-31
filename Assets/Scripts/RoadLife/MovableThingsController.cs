using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovableThingsController : MonoBehaviour
{
    public float removeDistance;
    public VehicleController Vehicle;
    public Background Background;
    public HashSet<ISpeedable> ThingsToSpeed = new HashSet<ISpeedable>();

    private void Start()
    {
        ThingsToSpeed.Add(Background);
    }

    public void Update()
    {
        var itemsToDelete = new List<ISpeedable>();
        foreach (var item in ThingsToSpeed)
        {
            item.SetSpeed(Vehicle.Speed);
            if (item.GetGameObject().transform.position.x < removeDistance)
            {
                itemsToDelete.Add(item);
            }
        }
        foreach (var item in itemsToDelete)
        {
            ThingsToSpeed.Remove(item);
            Destroy(item.GetGameObject());

        }
    }

}
