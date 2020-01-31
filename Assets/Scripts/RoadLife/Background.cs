using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour, ISpeedable
{
    private float speed;



    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void Update()
    {
        
    }

    public void Move()
    {

    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
