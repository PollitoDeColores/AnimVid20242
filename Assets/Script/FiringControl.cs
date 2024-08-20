using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FiringControl : MonoBehaviour
{
    Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void Fire(InputAction.CallbackContext context){
        bool _state = context.ReadValueAsButton();
        _animator.SetBool("Firing", _state);
    }
    public void OnShoot(){
        
    }
}
