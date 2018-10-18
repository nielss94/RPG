using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypes
{
    Melee,
    Ranged,
    Projectile,
    None
}

[CreateAssetMenu]
public class Weapon : EquippableItem {
    
    [Space]
    [Header("Weapon settings")]
    [SerializeField]
    private float range;
    [SerializeField]
    private WeaponTypes weaponType;
    

    public float Range
    {
        get
        {
            return range;
        }

        set
        {
            range = value;
        }
    }

    public WeaponTypes WeaponType
    {
        get
        {
            return weaponType;
        }

        set
        {
            weaponType = value;
        }
    }

    public virtual Damage GetDamage(Character c)
    {
        //TODO: Calculate using the character's stats
        //TODO: Damage range
        //TODO: Critical hits?
        Damage d = new Damage((int)Mathf.Round((MagicalAttack * 1.5f)), (int)Mathf.Round((PhysicalAttack * 1.5f)));
        return d;
    }
    

    public override string ToString()
    {
        return  Name + " \n"
            +   "MATT: " + MagicalAttack.ToString() + "\n"
            +   "PATT: " + PhysicalAttack.ToString() + "\n"
            +   "Range: " + range.ToString();
    }
}
