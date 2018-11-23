using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private new string name;
    [SerializeField]
    private Health health;
    [SerializeField]
    private Mana mana;
    [SerializeField]
    private Experience experience;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }
    
    public Experience Experience
    {
        get
        {
            return experience;
        }

        set
        {
            experience = value;
        }
    }

    public Health Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public Mana Mana
    {
        get
        {
            return mana;
        }

        set
        {
            mana = value;
        }
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
