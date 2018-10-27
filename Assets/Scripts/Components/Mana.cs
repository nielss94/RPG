using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mana {

    [SerializeField]
    private short curMana;
    [SerializeField]
    private short maxMana;
    [SerializeField]
    private short baseMaxMana;

    public short BaseMaxMana
    {
        get
        {
            return baseMaxMana;
        }

        set
        {
            baseMaxMana = value;
        }
    }

    public short MaxMana
    {
        get
        {
            return maxMana;
        }

        set
        {
            maxMana = value;
        }
    }

    public short CurMana
    {
        get
        {
            return curMana;
        }

        set
        {
            curMana = value;
        }
    }
}
