using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour {

    public AbilitiesPanel abilitiesPanel;
    public ActionBarPanel actionBarPanel;


    public void OpenAbilitiesPanel()
    {
        abilitiesPanel.gameObject.SetActive(true);
        actionBarPanel.gameObject.SetActive(false);
    }

    public void OpenActionBarPanel()
    {
        abilitiesPanel.gameObject.SetActive(false);
        actionBarPanel.gameObject.SetActive(true);
    }
}
