using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class AimController : MonoBehaviour
{
    [SerializeField] private AimConstraint chestAim;
    [SerializeField] private Transform aimRig;
    [SerializeField] private Transform _camera;
    Animator _animator;
    bool aiming;

    public void Aim(InputAction.CallbackContext ctx)
    {
        bool val = ctx.ReadValueAsButton();
        aiming = val;
        chestAim.enabled = val;
        aimRig.gameObject.SetActive(val); 
        _animator.SetBool("Aiming", val);
    }

    private void Awake()
    {
        aimRig.gameObject.SetActive(false); 
        _animator = GetComponent<Animator>();	
    }

    void Update()
    {
        if(aiming)
            transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(_camera.forward, transform.up).normalized);
    }
}
