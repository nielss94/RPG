using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour {

    public TextMeshProUGUI HealthText;
    public Image HealthBar;


    public Health Health;

    void OnValidate()
    {
        HealthText = GetComponentInChildren<TextMeshProUGUI>();
        HealthBar = GetComponentsInChildren<Image>()[1];
    }

    public void SetDisplayValues()
    {
        HealthText.text = Health.CurHealth + "/" + Health.MaxHealth;

        HealthBar.transform.localScale = new Vector3((float)Health.CurHealth / (float)Health.MaxHealth, HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
    }
}
