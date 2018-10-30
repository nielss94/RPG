using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitiesBar : MonoBehaviour {

    [SerializeField] AbilitySlotDisplay[] abilitySlotDisplays;

    [SerializeField]
    private AbilitySlot[] abilitySlots;
    

    private void OnValidate()
    {
        abilitySlotDisplays = transform.Find("AbilitySlots").GetComponentsInChildren<AbilitySlotDisplay>();
    }

    public void SetAbilitySlots(AbilitySlot[] abilitySlots)
    {
        this.abilitySlots = abilitySlots;
    }
    
    public void UpdateAbilitySlotDisplayValues()
    {
        for (int i = 0; i < abilitySlotDisplays.Length; i++)
        {
            if(abilitySlots[i] != null)
            {
                abilitySlotDisplays[i].AbilitySlot = abilitySlots[i];
                abilitySlotDisplays[i].SetDisplayValues();
            }
        }
    }
}
