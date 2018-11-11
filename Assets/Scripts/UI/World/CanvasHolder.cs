using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHolder : MonoBehaviour {

    public MinimapPanel MinimapPanel;
    public FadeScreen FadeScreen;
    public PlayerPanel PlayerPanel;
    public StatPanel StatPanel;
    public EquipmentPanel EquipmentPanel;
    public Inventory Inventory;
    public BuffPanel BuffPanel;
    public Spellbook Spellbook;
    public AbilitiesBar AbilitiesBar;
        
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
