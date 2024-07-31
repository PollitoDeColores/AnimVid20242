using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MovementDamp
{
    private Vector2 currentValue, targetValue, velocity;
    [SerializeField] private float smoothTime;

    public void DoSomething()
    {
        currentValue = Vector2.SmoothDamp(currentValue, targetValue, ref velocity, smoothTime);
    }

    public Vector2 CurrentValue => currentValue;
    public Vector2 TargetValue
    {
        set => targetValue = value;
    }
}
