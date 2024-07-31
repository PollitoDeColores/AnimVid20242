using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private MovementDamp motion;
    Animator animator;
    int animatorXID;
    int animatorYID;

    public void Movement(CallbackContext context)
    {
        Vector2 motionDirection = context.ReadValue<Vector2>();
        motion.TargetValue = motionDirection;
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

}
