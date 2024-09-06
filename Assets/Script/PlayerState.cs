using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaRegen = 10f;
    private float stamina;

    void Awake()
    {
        stamina = maxStamina;
    }

    void Update()
    {
        stamina += Time.deltaTime * staminaRegen;
        stamina = Mathf.Min(stamina, maxStamina);
    }

    public bool UpdateStamina(float _staminaChange){
        if(stamina >= Mathf.Abs(_staminaChange)){
            stamina += _staminaChange;
            return true;
        }
        else return false;
    }

    public float Stamina => stamina;



}
