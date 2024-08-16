using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class AimController : MonoBehaviour
{
    [SerializeField] private AimConstraint chestAim;
    [SerializeField] private Transform aimRig;
    [SerializeField] private Transform _camera;

    public void Aim(InputAction.CallbackContext ctx)
    {
        bool val = ctx.ReadValueAsButton();
        chestAim.enabled = val;
        aimRig.gameObject.SetActive(val); 
    }

    private void Awake()
    {
        aimRig.gameObject.SetActive(false); 
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(_camera.forward, transform.up).normalized);
    }
}
