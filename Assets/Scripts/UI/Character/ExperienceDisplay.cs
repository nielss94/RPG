using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExperienceDisplay : MonoBehaviour {

    public TextMeshProUGUI ExperienceText;
    public TextMeshProUGUI LevelText;
    public Image ExperienceBar;

    public Experience Experience;
	
    void OnValidate()
    {
        ExperienceText = GetComponentsInChildren<TextMeshProUGUI>()[0];
        LevelText = GetComponentsInChildren<TextMeshProUGUI>()[1];
        ExperienceBar = GetComponentsInChildren<Image>()[1];
    }

    public void SetDisplayValues()
    {
        ExperienceText.text = string.Format("{0} / {1} ({2}%)",Experience.ProgressToNextLevel,Experience.ExperienceToNextLevel, (int)(((float)Experience.ProgressToNextLevel / (float)Experience.ExperienceToNextLevel) * 100));
        ExperienceBar.transform.localScale = new Vector3((float)Experience.ProgressToNextLevel / (float)Experience.ExperienceToNextLevel, ExperienceBar.transform.localScale.y, ExperienceBar.transform.localScale.z);
        LevelText.text = string.Format("Lv. {0}",Experience.Level.ToString());
    }
}
