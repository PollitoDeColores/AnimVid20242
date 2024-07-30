using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Animator animator;
    int animatorXID;
    int animatorYID;

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

    public void Move(Vector3 motionDirection)
    {
        animator.SetFloat(animatorYID, motionDirection.y);
        animator.SetFloat(animatorXID, motionDirection.x);
    }
}
