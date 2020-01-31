using UnityEngine;
using UnityEditor;
using System;

public class CollisionEventor : MonoBehaviour
{
    public event Action<Collision2D> OnCollide = delegate { };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollide.Invoke(collision);
    }
}