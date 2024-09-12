using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{

    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaRegen = 10f;

    public float stamina;

    private void Awake()
    {
        stamina = maxStamina;
    }

    private void Update()
    {
        
        stamina = Time.deltaTime * staminaRegen;
        stamina = Mathf.Min(stamina, maxStamina);

    }

    // Se puede usar este método para aplicar la regen de stamina, ej: al consumir un colectible, etc
    public bool UpdateStamina(float staminaDelta)
    {
        if(stamina >= Mathf.Abs(staminaDelta))
        {
            stamina += staminaDelta;
            return true;    
        }

        return false;
    }

    public float Stamine => stamina;

}
