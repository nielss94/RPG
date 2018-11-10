using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityToolTip : MonoBehaviour {

    public static AbilityToolTip Instance;

    [SerializeField] Text nameText;
    [SerializeField] Text infoText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        gameObject.SetActive(false);
    }

    public void ShowTooltip(Ability abilityToShow)
    {
        gameObject.SetActive(true);

        nameText.text = abilityToShow.Name;
        infoText.text = abilityToShow.Description;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
