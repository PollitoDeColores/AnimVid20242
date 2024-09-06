using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]

public class Attack : MonoBehaviour
{
    private Animator anim;


    private bool AttackActive()
    {

        return anim.GetFloat("ActiveAttack") > 0.5f; 
    }
    public void LightAttack(InputAction.CallbackContext ctx)
    {
        if(!gameObject.scene.IsValid()) return;
        if (ctx.ReadValueAsButton()) return; 
        if(!GetComponent<PlayerState>().UpdateStamina(-15f)) return;
        if (AttackActive()) return; 

        anim.SetTrigger("Attack"); 
        anim.SetBool("HeavyAttack", false);
    }

    public void HeavyAttack(InputAction.CallbackContext ctx)
    {
        bool clicked = ctx.ReadValueAsButton();
        if(GetComponent<PlayerState>().Stamina < -25f) return;
        if (AttackActive()) return; 

        if(clicked)
        {
            anim.SetTrigger("Attack");
            anim.SetBool("HeavyAttack", true);
            anim.SetFloat("Charging", 1); 
        }
        else
        {
            anim.SetFloat("Charging", 0); 
            //  GetComponent<PlayerState>().UpdateStamina(-25f);
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
