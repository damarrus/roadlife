using UnityEngine;
using UnityEditor;

public interface ISpeedable
{
    GameObject GetGameObject();
    void SetSpeed(float speed);
}