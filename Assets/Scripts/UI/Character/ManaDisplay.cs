using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaDisplay : MonoBehaviour {

    public TextMeshProUGUI ManaText;
    public Image ManaBar;

    public Mana Mana;

    void OnValidate()
    {
        ManaText = GetComponentInChildren<TextMeshProUGUI>();
        ManaBar = GetComponentsInChildren<Image>()[1];
    }

    public void SetDisplayValues()
    {
        ManaText.text = Mana.CurMana + "/" + Mana.MaxMana;
        ManaBar.transform.localScale = new Vector3((float)Mana.CurMana / (float)Mana.MaxMana, ManaBar.transform.localScale.y, ManaBar.transform.localScale.z);
    }
}
