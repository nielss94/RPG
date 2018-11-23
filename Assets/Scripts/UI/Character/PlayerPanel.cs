using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanel : MonoBehaviour {

    public HealthDisplay healthDisplay;
    public ManaDisplay manaDisplay;
    public ExperienceDisplay experienceDisplay;

    void OnValidate()
    {
        healthDisplay = GetComponentInChildren<HealthDisplay>();
        manaDisplay = GetComponentInChildren<ManaDisplay>();
        experienceDisplay = GetComponentInChildren<ExperienceDisplay>();
    }
    
    public void SetResources(Health health, Mana mana, Experience experience)
    {
        healthDisplay.Health = health;
        manaDisplay.Mana = mana;
        experienceDisplay.Experience = experience;
    }

    public void UpdateDisplayValues()
    {
        healthDisplay.SetDisplayValues();
        manaDisplay.SetDisplayValues();
        experienceDisplay.SetDisplayValues();
    }
}
