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

    public override string ToString()
    {
        return base.ToString();
    }
}
