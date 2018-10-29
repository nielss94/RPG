﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class AbilitiesPanel : MonoBehaviour {

    [SerializeField] AbilityDisplay[] abilityDisplays;

    [SerializeField]
    private List<Ability> knownAbilities = new List<Ability>();
    [SerializeField]
    private List<Ability> unlockedAbilities = new List<Ability>();



    public int currentPage;
    public int cardsPerPage;

    public Text currentPageText;
    

    private void OnValidate()
    {
        SetPageCards();
    }

    public void NextPage()
    {
        int maxPage = (int)Mathf.Ceil(((float)abilityDisplays.Length / cardsPerPage));
        if (currentPage < maxPage)
        {
            currentPage++;
            currentPageText.text = currentPage.ToString();
            SetPageCards();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 1){
            currentPage--;
            currentPageText.text = currentPage.ToString();
            SetPageCards();
        }
    }

    public void SaveAbilities()
    {
        abilityDisplays = transform.Find("CardSlots").GetComponentsInChildren<AbilityDisplay>();
        UpdateAbilityCardValues();
    }

    public void SetPageCards()
    {
        int start = (currentPage - 1) * cardsPerPage;
        for (int i = 0; i < abilityDisplays.Length; i++)
        {
            if(i >= start && i < (start + cardsPerPage))
            {
                abilityDisplays[i].gameObject.SetActive(true);
            }else
            {
                abilityDisplays[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetAbilities(List<Ability> abilities)
    {
        knownAbilities = abilities;
    }

    public void SetUnlockedAbilities(List<Ability> unlocked)
    {
        unlockedAbilities = unlocked;
    }
    
    public void UpdateAbilityCardValues()
    {
        for (int i = 0; i < abilityDisplays.Length; i++)
        {
            Ability a = null;
            
            if(unlockedAbilities.Count > 0)
            {
                a = unlockedAbilities.FirstOrDefault(s => s.Name == abilityDisplays[i].Ability.Name);
            }

            if(a != null)
            {
                abilityDisplays[i].SetAbilityLock(false);
                abilityDisplays[i].SetAbilityCrafted(false);
            }
            else
            {
                Ability b = null;
                if (knownAbilities.Count > 0)
                {
                    b = knownAbilities.FirstOrDefault(s => s.Name == abilityDisplays[i].Ability.Name);
                }

                if(b != null)
                {
                    abilityDisplays[i].SetAbilityLock(false);
                    abilityDisplays[i].SetAbilityCrafted(true);
                }else
                {
                    abilityDisplays[i].SetAbilityLock(true);
                    abilityDisplays[i].SetAbilityCrafted(false);
                }
            }

            abilityDisplays[i].SetDisplayValues();
        }
    }
}