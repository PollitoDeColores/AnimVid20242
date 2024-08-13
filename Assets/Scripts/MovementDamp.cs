using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MovementDamp
{
    private Vector2 currentValue, targetValue, velocity;
    [SerializeField] private float smoothTime;
    [SerializeField] private float clampMagnitude;
    private bool clamp;

    public MovementDamp(bool clamp)
    {
        currentValue = Vector2.zero;
        targetValue = Vector2.zero;
        velocity = Vector2.zero;
        smoothTime = 0;
        clampMagnitude = 0;
        this.clamp = clamp;
    }

    public void DoSomething()
    {
        currentValue = Vector2.SmoothDamp(currentValue, 
            clamp ? Vector2.ClampMagnitude(targetValue, clampMagnitude): targetValue, ref velocity, smoothTime);
    }

    public Vector2 CurrentValue => currentValue;
    public Vector2 TargetValue{ set => targetValue = value;}
    public bool Clamp { set => clamp = value;}
}
