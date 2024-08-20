using UnityEngine;
using UnityEngine.InputSystem;

public class LookController : MonoBehaviour
{

    [SerializeField] private VectorDampener lookVector;
    [SerializeField] private Transform lookRig;
    [SerializeField] private float sensitivity;
    [SerializeField] private Vector2 _clamp;
    private float vertRotation;

    public void Look(InputAction.CallbackContext ctx)
    {
        lookVector.TargetValue = ctx.ReadValue<Vector2>() / new Vector2(Screen.width, Screen.height);

    }

    private void Update()
    {
        lookVector.Update();
        lookRig.RotateAround(lookRig.position, transform.up, lookVector.CurrentValue.x * sensitivity * 360f);
        vertRotation -= lookVector.CurrentValue.y*sensitivity*360;
        vertRotation = Mathf.Clamp(vertRotation, _clamp.x, _clamp.y);
        Vector3 _euler = lookRig.localEulerAngles;
        lookRig.localEulerAngles = new Vector3(vertRotation, _euler.y, _euler.z);
        //lookRig.RotateAround(lookRig.position, lookRig.right, lookVector.CurrentValue.x * sensitivity * 360f);
    }

}
