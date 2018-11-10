using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuffDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Ability ability;
    public float duration;

    public void Initialize(Ability ability)
    {
        this.ability = ability;
        duration = ability.Duration;
        GetComponent<Image>().sprite = this.ability.Image;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AbilityToolTip.Instance.ShowTooltip(ability);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AbilityToolTip.Instance.HideTooltip();
    }

    void Update()
    {
        if(ability != null)
        {
            float percent = (ability.Duration / duration);
            transform.GetChild(0).localScale = new Vector3(transform.GetChild(0).localScale.x, percent, transform.GetChild(0).localScale.z);
        }
    }
}
