using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using CallbackContent = UnityEngine.InputSystem.InputAction.CallbackContext; 

public class Controller : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform cameraTransform; 
    [SerializeField] private VectorDampener motionVector; /* = new VectorDampener(true);   */


    private int VelXid; 
    private int VelYid;

    public void Move(CallbackContent ctx)
    {
        Vector2 direction = ctx.ReadValue<Vector2>();
        motionVector.TargetValue = direction; 
    }

    public void ToggleSprint(CallbackContent ctx)
    {
        bool val = ctx.ReadValueAsButton();
        motionVector.Clamp = !val; 
    }

    private void Awake()
    {
        VelXid = Animator.StringToHash("VelX");
        VelYid = Animator.StringToHash("VelY"); 
    }

    private void Update()
    {
        motionVector.Update();
        Vector2 direction = motionVector.CurrentValue; 
        anim.SetFloat(VelXid, direction.x);
        anim.SetFloat(VelYid, direction.y);
    }

    public void OnAnimatorMove()
    {
        float interpolator = Mathf.Abs(Vector3.Dot(cameraTransform.forward, transform.up)); 
        Vector3 forward = Vector3.Lerp(cameraTransform.forward, cameraTransform.up, interpolator);
        Vector3 projected = Vector3.ProjectOnPlane(forward, transform.up);
        Quaternion rotation = Quaternion.LookRotation(projected, transform.up); 
        anim.rootRotation = Quaternion.Slerp(anim.rootRotation, rotation, motionVector.CurrentValue.magnitude);
        anim.ApplyBuiltinRootMotion();

    }
}
