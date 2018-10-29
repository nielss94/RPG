using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class AbilityDisplay : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image SpellImage;
    public Image CardImage;
    public Text AbilityName;
    public bool locked = true;
    public bool crafted = false;
    public bool inOtherPanel = false;

    public Ability Ability;


    private void OnValidate()
    {
        Text text = GetComponentInChildren<Text>();
        Image[] image = GetComponentsInChildren<Image>();
        
        SpellImage = image[0];
        CardImage = image[1];
        AbilityName = text;
    }
    
    public void SetAbilityLock(bool locked)
    {
        this.locked = locked;
    }

    public void SetDisplayValues()
    {
        SpellImage.sprite = Ability.Image;
        AbilityName.text = Ability.Name;
        if (locked)
        {
            CardImage.sprite = Resources.Load<Sprite>("Sprites/CardLocked");
        }
        else if (!crafted)
        {
            CardImage.sprite = Resources.Load<Sprite>("Sprites/UncraftedCard");
        } else
        {
            CardImage.sprite = Resources.Load<Sprite>("Sprites/Card");
        }
    }

    public void SetAbilityCrafted(bool crafted)
    {
        this.crafted = crafted;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //If locked, open unlock screen or message that says you need recipe
        //If unlocked but not available, open recipe screen
        if(locked)
        {
            UIController.OpenUnlockPanel(Ability);
        }
        else if(!crafted)
        {
            UIController.OpenCraftPanel(Ability);
        }
        else
        {
            print("Hover");
        }
        //If available, start hovering so it can be attached to healthbar
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //print(Ability.Name + " Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //print(Ability.Name + " Up");
    }
}
