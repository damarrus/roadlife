﻿using UnityEngine;
using System.Collections;
using System;

public class Timer : MonoBehaviour
{

    public event Action OnEnd = delegate { };

    public float MaxDur;
    [HideInInspector] public float Dur;
    [HideInInspector] public bool IsEventSended;
    [HideInInspector] public bool IsTimerActive;

    public void Update()
    {
        Dur -= Time.deltaTime;
        if (!IsEventSended && IsTimerActive && Dur < 0)
        {
            OnEnd.Invoke();
            IsEventSended = true;
            IsTimerActive = false;
        }
    }

    public void StartTimer()
    {
        Dur = MaxDur;
        IsEventSended = false;
        IsTimerActive = true;
    }


}
