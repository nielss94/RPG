using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinimapPanel : MonoBehaviour {

    public TextMeshProUGUI MapText;
    private string currentMap;

	void OnValidate()
    {
        MapText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetCurrentMap(string currentMap)
    {
        this.currentMap = currentMap;
        MapText.text = this.currentMap;
    }
}
