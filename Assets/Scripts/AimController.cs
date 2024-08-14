using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class AimController : MonoBehaviour
{
    [SerializeField] Transform _aimRig;
    [SerializeField] private MovementDamp LookVector;
    private bool val = false;
    [SerializeField] private AimConstraint _chestAim;

    void Awake()
    {
        _aimRig.gameObject.SetActive(val);     
    }

    public void Aim(InputAction.CallbackContext context){
        val = context.ReadValueAsButton();
        _chestAim.enabled = val; 
    }

    public void Look(InputAction.CallbackContext context){
        LookVector.TargetValue = context.ReadValue<Vector2>();
    }

    private void Update() {
        LookVector.DoSomething();
        _aimRig.RotateAround(_aimRig.position, transform.up, LookVector.CurrentValue.x);
    }
}
