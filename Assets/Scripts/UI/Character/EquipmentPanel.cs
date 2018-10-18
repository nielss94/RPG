using System;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
	[SerializeField] Transform equipmentSlotsParent;
	[SerializeField] EquipmentSlot[] equipmentSlots;

	public event Action<Item> OnItemRightClickedEvent;

	private void Start()
	{
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			equipmentSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
		}
	}

	private void OnValidate()
	{
		equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
	}

    public bool HasWeapon()
    {
        foreach (var item in equipmentSlots)
        {
            if (item.EquipmentType == EquipmentTypes.Weapon)
            {
                return true;
            }
        }
        return false;
    }

    public Weapon GetWeapon()
    {
        if (HasWeapon())
        {
            foreach (var item in equipmentSlots)
            {
                if(item.EquipmentType == EquipmentTypes.Weapon)
                {
                    return (Weapon)item.Item;
                }
            }
        }
        return null;
    }

	public bool AddItem(EquippableItem item, out EquippableItem previousItem)
	{
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			if (equipmentSlots[i].EquipmentType == item.EquipmentType)
			{
				previousItem = (EquippableItem)equipmentSlots[i].Item;
				equipmentSlots[i].Item = item;
				return true;
			}
		}
		previousItem = null;
		return false;
	}

	public bool RemoveItem(EquippableItem item)
	{
		for (int i = 0; i < equipmentSlots.Length; i++)
		{
			if (equipmentSlots[i].Item == item)
			{
				equipmentSlots[i].Item = null;
				return true;
			}
		}
		return false;
	}
}
