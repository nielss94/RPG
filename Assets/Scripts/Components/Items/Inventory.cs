using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[SerializeField] List<Item> items;
	[SerializeField] Transform itemsParent;
	[SerializeField] ItemSlot[] itemSlots;

	public event Action<Item> OnItemRightClickedEvent;

    public ItemTooltip itemTooltip;

    public bool dragging = false;
    public ItemSlot draggingSlot;
    public ItemSlot hoveringSlot;

	private void Start()
	{
		for (int i = 0; i < itemSlots.Length; i++)
		{
			itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
		}
        OnValidate();
	}

    void OnEnable()
    {
        itemTooltip.gameObject.SetActive(true);
        itemTooltip.HideTooltip();
    }

    void OnDisable()
    {
        itemTooltip.gameObject.SetActive(false);
    }

	private void OnValidate()
	{
		if (itemsParent != null)
			itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();

		RefreshUI();
	}

    public void MoveItem()
    {
        Item dragSlotItem = draggingSlot.Item;
        Item hoverSlotItem = hoveringSlot.Item;

        if (hoverSlotItem != null)
            draggingSlot.Item = hoverSlotItem;
        else
            draggingSlot.Item = null;
        hoveringSlot.Item = dragSlotItem;
        draggingSlot = null;
        hoveringSlot = null;
    }

	private void RefreshUI()
	{
		int i = 0;
		for (; i < items.Count && i < itemSlots.Length; i++)
		{
			itemSlots[i].Item = items[i];
		}

		for (; i < itemSlots.Length; i++)
		{
			itemSlots[i].Item = null;
		}
	}

	public bool AddItem(Item item)
	{
		if (IsFull())
			return false;

		items.Add(item);
        foreach (var itemSlot in itemSlots)
        {
            if(itemSlot.Item == null)
            {
                itemSlot.Item = item;
                return true;
            }
        }
		//RefreshUI();
		return true;
	}

	public bool RemoveItem(Item item)
	{
		if (items.Remove(item))
		{
            foreach (var itemSlot in itemSlots)
            {
                if (itemSlot.Item == item)
                {
                    itemSlot.Item = null;
                    return true;
                }
            }
		}
		return false;
	}

	public bool IsFull()
	{
		return items.Count >= itemSlots.Length;
	}
}