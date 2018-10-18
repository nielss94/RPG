using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {

    [SerializeField]
    private EquippableItem helmet;
    [SerializeField]
    private EquippableItem body;
    [SerializeField]
    private EquippableItem leggings;
    [SerializeField]
    private EquippableItem boots;
    [SerializeField]
    private EquippableItem gloves;
    [SerializeField]
    private EquippableItem shield;

    void Awake()
    {
        if (!helmet) 
            helmet = Resources.Load<EquippableItem>("Prefabs/Armour/NoArmour");
        if (!body)
            body = Resources.Load<EquippableItem>("Prefabs/Armour/NoArmour");
        if (!leggings)
            leggings = Resources.Load<EquippableItem>("Prefabs/Armour/NoArmour");
        if (!boots)
            boots = Resources.Load<EquippableItem>("Prefabs/Armour/NoArmour");
        if (!gloves)
            gloves = Resources.Load<EquippableItem>("Prefabs/Armour/NoArmour");
        if (!shield)
            shield = Resources.Load<EquippableItem>("Prefabs/Armour/NoArmour");
    }

    public EquippableItem Gloves
    {
        get
        {
            return gloves;
        }

        set
        {
            gloves = value.EquipmentType == EquipmentTypes.Gloves ? value : gloves;
        }
    }

    public EquippableItem Boots
    {
        get
        {
            return boots;
        }

        set
        {
            boots = value.EquipmentType == EquipmentTypes.Boots ? value : boots;
        }
    }

    public EquippableItem Leggings
    {
        get
        {
            return leggings;
        }

        set
        {
            leggings = value.EquipmentType == EquipmentTypes.Leggings ? value : leggings;
        }
    }

    public EquippableItem Body
    {
        get
        {
            return body;
        }

        set
        {
            body = value.EquipmentType == EquipmentTypes.Body ? value : body;
        }
    }

    public EquippableItem Helmet
    {
        get
        {
            return helmet;
        }

        set
        {
            helmet = value.EquipmentType == EquipmentTypes.Helmet ? value : helmet;
        }
    }

    public EquippableItem Shield
    {
        get
        {
            return shield;
        }

        set
        {
            shield = value.EquipmentType == EquipmentTypes.Shield ? value : shield;
        }
    }


    public int GetTotalMagicalDefense()
    {
        return helmet.MagicalDefense + body.MagicalDefense + leggings.MagicalDefense + boots.MagicalDefense + gloves.MagicalDefense + shield.MagicalDefense;
    }

    public int GetTotalPhysicalDefense()
    {
        return helmet.PhysicalDefense + body.PhysicalDefense + leggings.PhysicalDefense + boots.PhysicalDefense + gloves.PhysicalDefense + shield.PhysicalDefense;
    }

    public override string ToString()
    {
        return string.Format("Helmet: {0} \n"
                            + "Body: {1} \n"
                            + "Leggings: {2} \n"
                            + "Boots: {3} \n"
                            + "Gloves: {4} \n"
                            + "Shield: {5} \n", helmet.ToString(), body.ToString(), leggings.ToString(), boots.ToString(), gloves.ToString(), shield.ToString());
    }
}
