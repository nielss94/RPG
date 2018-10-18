using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage {

    private int magicalAttack;
    private int physicalAttack;
    private int totalAttack;

    public Damage(int mAtt, int pAtt)
    {
        magicalAttack = mAtt;
        physicalAttack = pAtt;
        totalAttack = mAtt + pAtt;
    }

    public int MagicalAttack
    {
        get
        {
            return magicalAttack;
        }

        set
        {
            magicalAttack = value;
        }
    }

    public int PhysicalAttack
    {
        get
        {
            return physicalAttack;
        }

        set
        {
            physicalAttack = value;
        }
    }

    public int GetMagicPercentage()
    {
        return (int)Mathf.Ceil(magicalAttack / totalAttack * 100);
    }

    public int GetPhysicalPercentage()
    {
        return (int)Mathf.Ceil(physicalAttack / totalAttack * 100);
    }

    public int GetTotalDamage()
    {
        return MagicalAttack + PhysicalAttack;
    }
}
