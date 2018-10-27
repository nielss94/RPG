using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    
	void Awake()
    {
        FloatingTextController.Initialize();
    }

    public static void OpenUnlockPanel(Ability ability)
    {
        if (GameObject.Find("UnlockPanel") == null)
        {
            UnlockPanel unlockPanel = Resources.Load<UnlockPanel>("Prefabs/UI/Spellbook/UnlockPanel");
            UnlockPanel up = Instantiate(unlockPanel, GameObject.Find("Canvas").transform);
            up.gameObject.name = "UnlockPanel";
            up.Initialize(ability);
        }
        else
        {
            print("Another unlock panel is opened");
        }
    }
}
