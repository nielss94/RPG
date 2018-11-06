using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    [SerializeField] Transform currencyParent;
    [SerializeField] CurrencyDisplay[] currencyDisplays;
    private Currency currency;

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
        if (currencyParent != null)
            currencyDisplays = currencyParent.GetComponentsInChildren<CurrencyDisplay>();

		RefreshUI();
	}

    public void UpdateCurrency()
    {
        if(currencyDisplays.Length == 4)
        {
            currencyDisplays[0].value.text = currency.Copper.ToString();
            currencyDisplays[1].value.text = currency.Silver.ToString();
            currencyDisplays[2].value.text = currency.Gold.ToString();
            currencyDisplays[3].value.text = currency.Platinum.ToString();
        }else
        {
            Debug.LogError("4 Currency Types are expected to be found, please make sure there aren't less or more");
        }
    }

    public void MoveItem()
    {
        Item dragSlotItem = draggingSlot.Item;
        Item hoverSlotItem = hoveringSlot.Item;
        int dragSlotQuantity = draggingSlot.ItemQuantity;
        int hoverSlotQuantity = hoveringSlot.ItemQuantity;

        if (hoverSlotItem != null)
        {
            draggingSlot.Item = hoverSlotItem;
            draggingSlot.ItemQuantity = hoverSlotQuantity;
        }
        else
        {
            draggingSlot.Item = null;
            draggingSlot.ItemQuantity = 1;
        }
        hoveringSlot.Item = dragSlotItem;
        hoveringSlot.ItemQuantity = dragSlotQuantity;
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

	public bool AddItem(Item item, int quantity = 1)
	{
        ItemSlot itemFound = itemSlots.FirstOrDefault(s => (s.Item != null ? s.Item.Name : "") == item.Name);

        if(itemFound != null && item.Stackable)
        {
            itemFound.ItemQuantity += quantity;
            return true;
        }
        else
        {
		    if (IsFull())
			    return false;

            if (item.Stackable)
            {
                items.Add(item);
                foreach (var itemSlot in itemSlots)
                {
                    if(itemSlot.Item == null)
                    {
                        itemSlot.Item = item;
                        itemSlot.ItemQuantity = quantity;
                        return true;
                    }
                }
            }
            else
            {
                quantity = quantity > (itemSlots.Length - items.Count) ? itemSlots.Length - items.Count : quantity;
                for (int i = 0; i < quantity; i++)
                {
                    items.Add(item);
                    foreach (var itemSlot in itemSlots)
                    {
                        if (itemSlot.Item == null)
                        {
                            itemSlot.Item = item;
                            itemSlot.ItemQuantity = 1;
                            break;
                        }
                    }
                }
                return true;
            }
            return true;
        }
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

    public void SetCurrency(Currency currency)
    {
        this.currency = currency;
    }

	public bool IsFull()
	{
		return items.Count >= itemSlots.Length;
	}
}