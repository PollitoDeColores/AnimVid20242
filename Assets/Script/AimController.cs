using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class AimController : MonoBehaviour
{
    [SerializeField] private AimConstraint chestAim;
    [SerializeField] private Transform aimRig;
    [SerializeField] private VectorDampener lookVector; 

    public void Aim(InputAction.CallbackContext ctx)
    {
        bool val = ctx.ReadValueAsButton();
        chestAim.enabled = val;
        aimRig.gameObject.SetActive(val); 
    }

    public void Look(InputAction.CallbackContext ctx) 
    {
        lookVector.TargetValue = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        lookVector.Update();
        aimRig.RotateAround(aimRig.position, transform.up, lookVector.CurrentValue.x);
    }

    private void Awake()
    {
        aimRig.gameObject.SetActive(false); 
    }
}
