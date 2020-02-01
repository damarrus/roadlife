using UnityEngine;
using System.Collections;

public class ColliderThing : MonoBehaviour
{

    public string AreaId;
    public RectTransform rectTransform;
    public BoxCollider2D boxCollider2D;

    private void Start()
    {
        boxCollider2D.size = rectTransform.rect.size;
    }
}
