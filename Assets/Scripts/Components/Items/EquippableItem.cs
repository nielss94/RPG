using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentTypes
{
    None,
    Helmet,
    Body,
    Leggings,
    Boots,
    Gloves,
    Shield,
    Weapon
}


[CreateAssetMenu(menuName = "RPG/EquippableItem")]
public class EquippableItem : Item{

    [SerializeField]
    private int physicalAttack;
    [SerializeField]
    private int magicalAttack;
    [SerializeField]
    private short physicalDefense;
    [SerializeField]
    private short magicalDefense;
    [SerializeField]
    private EquipmentTypes equipmentType;

    [SerializeField]
    private short strengthBonus;
    [SerializeField]
    private short intelligenceBonus;
    [SerializeField]
    private short vitalityBonus;
    [SerializeField]
    private short agilityBonus;
    [SerializeField]
    private float strengthPercentBonus;
    [SerializeField]
    private float intelligencePercentBonus;
    [SerializeField]
    private float vitalityPercentBonus;
    [SerializeField]
    private float agilityPercentBonus;

    
    public void Equip(PlayableCharacter c)
    {
        if (PhysicalAttack != 0)
            c.Stats.PhysicalAttack.AddModifier(new StatModifier(PhysicalAttack, StatModType.Flat, this));
        if (MagicalAttack != 0)
            c.Stats.MagicalAttack.AddModifier(new StatModifier(MagicalAttack, StatModType.Flat, this));
        if (PhysicalDefense != 0)
            c.Stats.PhysicalDefense.AddModifier(new StatModifier(PhysicalDefense, StatModType.Flat, this));
        if (MagicalDefense != 0)
            c.Stats.MagicalDefense.AddModifier(new StatModifier(MagicalDefense, StatModType.Flat, this));
        if (StrengthBonus != 0)
            c.Stats.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
        if (AgilityBonus != 0)
            c.Stats.Agility.AddModifier(new StatModifier(AgilityBonus, StatModType.Flat, this));
        if (IntelligenceBonus != 0)
            c.Stats.Intelligence.AddModifier(new StatModifier(IntelligenceBonus, StatModType.Flat, this));
        if (VitalityBonus != 0)
            c.Stats.Vitality.AddModifier(new StatModifier(VitalityBonus, StatModType.Flat, this));

        if (StrengthPercentBonus != 0)
            c.Stats.Strength.AddModifier(new StatModifier(StrengthPercentBonus, StatModType.PercentMult, this));
        if (AgilityPercentBonus != 0)
            c.Stats.Agility.AddModifier(new StatModifier(AgilityPercentBonus, StatModType.PercentMult, this));
        if (IntelligencePercentBonus != 0)
            c.Stats.Intelligence.AddModifier(new StatModifier(IntelligencePercentBonus, StatModType.PercentMult, this));
        if (VitalityPercentBonus != 0)
            c.Stats.Vitality.AddModifier(new StatModifier(VitalityPercentBonus, StatModType.PercentMult, this));
    }

    public void Unequip(PlayableCharacter c)
    {
        c.Stats.Strength.RemoveAllModifiersFromSource(this);
        c.Stats.Agility.RemoveAllModifiersFromSource(this);
        c.Stats.Intelligence.RemoveAllModifiersFromSource(this);
        c.Stats.Vitality.RemoveAllModifiersFromSource(this);
        c.Stats.MagicalAttack.RemoveAllModifiersFromSource(this);
        c.Stats.PhysicalAttack.RemoveAllModifiersFromSource(this);
        c.Stats.PhysicalDefense.RemoveAllModifiersFromSource(this);
        c.Stats.MagicalDefense.RemoveAllModifiersFromSource(this);
    }

    public short MagicalDefense
    {
        get
        {
            return magicalDefense;
        }

        set
        {
            magicalDefense = value;
        }
    }

    public short PhysicalDefense
    {
        get
        {
            return physicalDefense;
        }

        set
        {
            physicalDefense = value;
        }
    }

    public EquipmentTypes EquipmentType
    {
        get
        {
            return equipmentType;
        }

        set
        {
            equipmentType = value;
        }
    }

    public float AgilityPercentBonus
    {
        get
        {
            return agilityPercentBonus;
        }

        set
        {
            agilityPercentBonus = value;
        }
    }

    public float VitalityPercentBonus
    {
        get
        {
            return vitalityPercentBonus;
        }

        set
        {
            vitalityPercentBonus = value;
        }
    }

    public float IntelligencePercentBonus
    {
        get
        {
            return intelligencePercentBonus;
        }

        set
        {
            intelligencePercentBonus = value;
        }
    }

    public float StrengthPercentBonus
    {
        get
        {
            return strengthPercentBonus;
        }

        set
        {
            strengthPercentBonus = value;
        }
    }

    public short AgilityBonus
    {
        get
        {
            return agilityBonus;
        }

        set
        {
            agilityBonus = value;
        }
    }

    public short VitalityBonus
    {
        get
        {
            return vitalityBonus;
        }

        set
        {
            vitalityBonus = value;
        }
    }

    public short StrengthBonus
    {
        get
        {
            return strengthBonus;
        }

        set
        {
            strengthBonus = value;
        }
    }

    public short IntelligenceBonus
    {
        get
        {
            return intelligenceBonus;
        }

        set
        {
            intelligenceBonus = value;
        }
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

    public override string ToString()
    {
        return Name + " \n"
            + "MDEF: " + magicalDefense.ToString() + "\n"
            + "PDEF: " + physicalDefense.ToString() + "\n"
            + "Type: " + equipmentType.ToString();
    }
}
