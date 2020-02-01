using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class ControllerLever : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action<Quaternion> OnLeverUsed;
    private bool canRotate;
    private float maxAngle = 1080;
    private float temp;
    private float angle, sumAngle;
   
    public void Start()
    {

        temp = angle;
    }
    public void Update()
    {
        // if (sumAngle <= 1080)
        {


            if (canRotate)
            {

                angle = -Vector2.SignedAngle(Input.mousePosition - transform.position, Vector2.right);
                if (angle < 0)
                {
                    angle = 360 + angle;
                }
                //Debug.Log(transform.rotation.z);
                Debug.Log(temp);
                Debug.Log(angle); 

                if (angle > temp)
                {
                    Debug.Log("dsd");
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + 5);

                }
                temp = angle;
                //if (angle <= 0 && angle < temp)
                //{
                //    transform.rotation = Quaternion.Euler(0, 0, -angle);
                //    temp = angle;
                //}
                //else if (angle >= 0 && angle < temp)
                //{
                //    transform.rotation = Quaternion.Euler(0, 0, -angle);
                //    temp = angle;
                //}

                //if (transform.rotation.z > 0 && transform.rotation.z != temp)
                //{
                //    sumAngle += transform.rotation.z;
                //    temp = transform.rotation.z;
                //}
                //else if(transform.rotation.z != temp)
                //{
                //    sumAngle += -transform.rotation.z;
                //    temp = transform.rotation.z;
                //}

                //Debug.Log(sumAngle);
            }

            else if (!canRotate)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 3);
            }
        }
    }
  public void OnPointerDown(PointerEventData eventData)
    {
        canRotate = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        canRotate = false;
        
    }
}
