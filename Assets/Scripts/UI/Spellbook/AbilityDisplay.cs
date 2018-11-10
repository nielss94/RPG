using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class AbilityDisplay : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image SpellImage;
    public Image CardImage;
    public Text AbilityName;
    public Text CooldownText;
    public Text ManaCostText;
    public bool locked = true;
    public bool crafted = false;
    public bool inOtherPanel = false;

    public Ability Ability;

    public bool dragging = false;
    private Image spellIcon;
    private Image draggingSpell;

    void Start()
    {
        spellIcon = Resources.Load<Image>("Prefabs/UI/Spellbook/SpellIcon");
    }

    void OnDisable()
    {
        if (dragging)
            StopDragging();
    }

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        Image[] image = GetComponentsInChildren<Image>();
        SpellImage = image[0];
        CardImage = image[1];
        AbilityName = texts[0];
        CooldownText = texts[1];
        ManaCostText = texts[2];
    }
    
    public void SetAbilityLock(bool locked)
    {
        this.locked = locked;
    }

    public void SetDisplayValues()
    {
        SpellImage.sprite = Ability.Image;
        AbilityName.text = Ability.Name;
        CooldownText.text = "";
        ManaCostText.text = "";

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
            CooldownText.text = Ability.Cooldown.ToString();
            ManaCostText.text = Ability.Cost.ToString();
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
        //If available, start hovering so it can be attached to healthbar
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!locked && crafted)
        {
            dragging = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragging)
        {
            draggingSpell.rectTransform.position = eventData.position;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragging)
        {
            draggingSpell = Instantiate(spellIcon, eventData.position, Quaternion.identity,transform) as Image;
            draggingSpell.sprite = SpellImage.sprite;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StopDragging();
    }

    void StopDragging()
    {
        AbilitySlotDisplay asd = AbilitiesBar.GetHoveringAbility();
        if(asd != null)
        {
            asd.AbilitySlot.ability = Ability;
            asd.SetDisplayValues();
        }
        dragging = false;
        if(draggingSpell != null)
            Destroy(draggingSpell.gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!locked && crafted)
        {
            AbilityToolTip.Instance.ShowTooltip(Ability);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AbilityToolTip.Instance.HideTooltip();
    }
}
