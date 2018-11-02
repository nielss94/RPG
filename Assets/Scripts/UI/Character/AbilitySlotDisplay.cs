using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class AbilitySlotDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Image SpellImage;
    public Image CooldownImage;
    public TextMeshProUGUI Hotkey;
    public TextMeshProUGUI CooldownText;

    public AbilitySlot AbilitySlot;
    
    private void OnValidate()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        Image[] image = GetComponentsInChildren<Image>();

        SpellImage = image[0];
        CooldownImage = image[1];
        CooldownText = texts[0];
        Hotkey = texts[1];
    }
    
    public void SetDisplayValues()
    {
        if(AbilitySlot.ability != null)
        {
            SpellImage.sprite = AbilitySlot.ability.Image;
            Hotkey.text = AbilitySlot.hotKey.ToString();
        }
    }
    
    void Update()
    {
        if(AbilitySlot.cooldownLeft > 0)
        {
            CooldownImage.transform.localScale = new Vector3(CooldownImage.transform.localScale.x, AbilitySlot.cooldownLeft / AbilitySlot.ability.Cooldown, CooldownImage.transform.localScale.z);
            CooldownText.text = AbilitySlot.cooldownLeft.ToString("F1");
        }else
        {
            CooldownText.text = "";
            CooldownImage.transform.localScale = new Vector3(CooldownImage.transform.localScale.x, 0, CooldownImage.transform.localScale.z);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AbilitiesBar.SetHoveringAbility(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AbilitiesBar.SetHoveringAbility(null);
    }
    
}
