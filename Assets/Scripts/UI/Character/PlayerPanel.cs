using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanel : MonoBehaviour {

    public HealthDisplay healthDisplay;
    public ManaDisplay manaDisplay;

    void OnValidate()
    {
        healthDisplay = GetComponentInChildren<HealthDisplay>();
        manaDisplay = GetComponentInChildren<ManaDisplay>();
    }
    
    public void SetResources(Health health, Mana mana)
    {
        healthDisplay.Health = health;
        manaDisplay.Mana = mana;
    }

    public void UpdateDisplayValues()
    {
        healthDisplay.SetDisplayValues();
        manaDisplay.SetDisplayValues();
    }
}
