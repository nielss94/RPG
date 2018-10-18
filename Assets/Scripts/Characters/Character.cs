using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private new string name;
    [SerializeField]
    private short health;
    [SerializeField]
    private short maxHealth;
    [SerializeField]
    private short level;
    [SerializeField]
    private int experience;

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

    public short Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    public short Health
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

    public int Experience
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

    public override string ToString()
    {
        return base.ToString();
    }
}
