using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private MovementDamp motion = new MovementDamp(true);
    Animator animator;
    int animatorXID;
    int animatorYID;

    public void Movement(CallbackContext context)
    {
        Vector2 motionDirection = context.ReadValue<Vector2>();
        motion.TargetValue = motionDirection;
    }

    public void Sprint(CallbackContext context)
    {
        bool Sprint = context.ReadValueAsButton();
        motion.Clamp = !Sprint;
    }

    private void Update()
    {
        motion.DoSomething();
        animator.SetFloat(animatorYID, motion.CurrentValue.y);
        animator.SetFloat(animatorXID, motion.CurrentValue.x);
    }
    private void Awake()
    {
        animatorXID = Animator.StringToHash("X");
        animatorYID = Animator.StringToHash("Y");
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void OnAnimatorMove()
    {
        float interpolator = MathF.Abs(Vector3.Dot(Camera.main.transform.forward, this.transform.up));
        Vector3 Forward = Vector3.Lerp(Camera.main.transform.forward, Camera.main.transform.up, interpolator);
        Vector3 Projected = Vector3.ProjectOnPlane(Forward, this.transform.up);
        Quaternion rotation = Quaternion.LookRotation(Projected, this.transform.up);
        animator.rootRotation = Quaternion.Slerp(animator.rootRotation, rotation, motion.CurrentValue.magnitude);
        animator.ApplyBuiltinRootMotion();
    }

}
