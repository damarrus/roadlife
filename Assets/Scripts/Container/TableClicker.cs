using System;
using System.Linq;
using UnityEngine;

public class TableClicker : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;
    public RectTransform Rect;
    public Action<Vector2> OnTableClick = delegate { };

    private void Start()
    {
        boxCollider2D.size = Rect.rect.size;
    }

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
