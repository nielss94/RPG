using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health  {

    [SerializeField]
    private short curHealth;
    [SerializeField]
    private short maxHealth;
    [SerializeField]
    private short baseMaxHealth;

    public short CurHealth
    {
        get
        {
            return curHealth;
        }

        set
        {
            curHealth = value;
        }
    }

    public short MaxHealth
    {
        get
        {
            return maxHealth;
        }

        set
        {
            maxHealth = value;
        }
    }

    public short BaseMaxHealth
    {
        get
        {
            return baseMaxHealth;
        }

        set
        {
            baseMaxHealth = value;
        }
    }
}
