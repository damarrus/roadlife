using UnityEngine;
using System.Collections;

public class DropPickUpController : MonoBehaviour
{
    public Sign SignPrefab;
    public Transform dropStartPlace;
    public MovableThingsController MovableThingsController;

    public void CreateDrop(DropItem dropPrefab, float distance)
    {
        var drop = Instantiate(dropPrefab, dropStartPlace);
        drop.transform.Translate(new Vector3(distance, 0));
        MovableThingsController.ThingsToSpeed.Add(drop);
    }
    public void CreateSign(Sprite sprite, float distance)
    {
        var sign = Instantiate(SignPrefab, dropStartPlace);
        MovableThingsController.ThingsToSpeed.Add(sign);
    }
}
