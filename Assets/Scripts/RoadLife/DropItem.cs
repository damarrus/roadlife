using UnityEngine;
using System.Collections;

public abstract class DropItem : MonoBehaviour, ISpeedable
{
    public Rigidbody2D Rigidbody;

    public abstract void ApplyItemOnPickUp();

    public void SetSpeed(float speed)
    {
        Rigidbody.velocity = new Vector2(-speed * Time.deltaTime, 0);
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
