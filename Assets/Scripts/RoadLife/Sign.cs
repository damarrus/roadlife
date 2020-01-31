using UnityEngine;
using System.Collections;

public class Sign : MonoBehaviour, ISpeedable
{
    public SpriteRenderer SpriteRenderer;
    public TMPro.TextMeshProUGUI Text;
    public Rigidbody2D Rigidbody;

    public void Set(Sprite sprite, string text)
    {
        SpriteRenderer.sprite = sprite;
        Text.text = text;
    }

    public void SetSpeed(float speed)
    {
        Rigidbody.velocity = new Vector2(-speed * Time.deltaTime, 0);
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
