﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitiesBar : MonoBehaviour {

    [SerializeField] AbilitySlotDisplay[] abilitySlotDisplays;

    [SerializeField]
    private AbilitySlot[] abilitySlots;


    public AbilityToolTip abilityToolTip;

    private static AbilitySlotDisplay hoveringSlot;


    void OnEnable()
    {
        abilityToolTip.gameObject.SetActive(true);
        abilityToolTip.HideTooltip();
    }
    
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

    public static void SetHoveringAbility(AbilitySlotDisplay abilitySlotDisplay)
    {
        hoveringSlot = abilitySlotDisplay;
    }

    public static AbilitySlotDisplay GetHoveringAbility()
    {
        return hoveringSlot;
    }
}
