using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbilitiesPanel))]
public class AbilitiesPanelEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AbilitiesPanel myScript = (AbilitiesPanel)target;
        if (GUILayout.Button("Save Abilities"))
        {
            myScript.SaveAbilities();
        }
    }
}
