using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrencyDisplay : MonoBehaviour {
    
    public TextMeshProUGUI value;

    private void OnValidate()
    {
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        value = text;
    }
}
