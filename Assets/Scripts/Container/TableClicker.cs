using System;
using System.Linq;
using UnityEngine;

public class TableClicker : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;
    public Action<Vector2> OnTableClick = delegate { };

    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (boxCollider2D.OverlapPoint(Input.mousePosition))
            {
                OnTableClick.Invoke(Input.mousePosition);
            }
        }
    }
}
