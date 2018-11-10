using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    
    public static void Initialize()
    {
        FloatingTextController.Initialize();
    }

    public static void OpenUnlockPanel(Ability ability)
    {
        if (GameObject.Find("UnlockPanel") == null && GameObject.Find("CraftPanel") == null)
        {
            UnlockPanel unlockPanel = Resources.Load<UnlockPanel>("Prefabs/UI/Spellbook/UnlockPanel");
            UnlockPanel up = Instantiate(unlockPanel, GameObject.Find("Canvas").transform);
            up.gameObject.name = "UnlockPanel";
            up.Initialize(ability);
        }
        else
        {
            print("A craft panel or unlock panel is already opened");
        }
    }

    public static void OpenCraftPanel(Ability ability)
    {
        if (GameObject.Find("CraftPanel") == null && GameObject.Find("UnlockPanel") == null)
        {
            CraftPanel craftPanel = Resources.Load<CraftPanel>("Prefabs/UI/Spellbook/CraftPanel");
            CraftPanel cp = Instantiate(craftPanel, GameObject.Find("Canvas").transform);
            cp.gameObject.name = "CraftPanel";
            cp.Initialize(ability);
        }
        else
        {
            print("A craft panel or unlock panel is already opened");
        }
    }


}
