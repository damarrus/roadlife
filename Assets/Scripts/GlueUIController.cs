using UnityEngine;
using System.Collections;
using System;

public class GlueUIController : MonoBehaviour
{
    public Transform parentTrans;
    public MonoBehaviour gluePrefab;
    public ControllerGrue controllerGrue;

    public void Start()
    {
        controllerGrue.OnGlueUsed += pos => CreateEffect(pos);
    }

    private void CreateEffect(Vector2 pos)
    {
        Instantiate(gluePrefab, pos, Quaternion.identity, parentTrans);
    }
}
