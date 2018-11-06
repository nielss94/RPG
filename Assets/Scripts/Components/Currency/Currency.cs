using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Currency  {

    [SerializeField]
    private ulong value = 0;

    private int platinum;
    private int gold;
    private int silver;
    private int copper;

    public Currency(ulong value)
    {
        this.value = value;
        SetValues();
    }

    public void SetValues()
    {
        ulong v = value;
        platinum = (int)v / 1000000;
        v = v % 1000000;
        gold = (int)v / 10000;
        v = v % 10000;
        silver = (int)v / 100;
        copper = (int)v % 100;
    }

    public void AddValue(ulong value)
    {
        this.value += value;
        SetValues();
    }

    public bool SubtractValue(ulong value)
    {
        if(((int)this.value - (int)value) < 0)
        {
            return false;
        }
        this.value = (ulong)Mathf.Clamp((int)this.value - (int)value,0, long.MaxValue);
        SetValues();
        return true;
    }
    
    public override string ToString()
    {
        return "P: " + Platinum + ", G: " + Gold + ", S: " + Silver + ", C: " + Copper;
    }

    public int Platinum
    {
        get
        {
            return platinum;
        }

        set
        {
            platinum = value;
        }
    }

    public int Gold
    {
        get
        {
            return gold;
        }

        set
        {
            gold = value;
        }
    }

    public int Silver
    {
        get
        {
            return silver;
        }

        set
        {
            silver = value;
        }
    }

    public int Copper
    {
        get
        {
            return copper;
        }

        set
        {
            copper = value;
        }
    }

    public ulong Value
    {
        get
        {
            return value;
        }

        set
        {
            this.value = value;
        }
    }
}
