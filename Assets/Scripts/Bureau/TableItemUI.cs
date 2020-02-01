using UnityEngine;
using System.Collections;
using System;

public class TableItemUI : MonoBehaviour
{
    public event Action OnBreakEnd = delegate { };
    public Animator animator;
    [HideInInspector] public bool IsBreaking;
    public static string BREAK = "Break";

    public void Break()
    {
        IsBreaking = true;
        animator.SetTrigger(BREAK);

    }

    //From animator
    public void OnBreakAnimEnd()
    {
        IsBreaking = false;
        OnBreakEnd.Invoke();
    }

}
