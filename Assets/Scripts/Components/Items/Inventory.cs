using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] List<Item> items;
	[SerializeField] Transform itemsParent;
	[SerializeField] ItemSlot[] itemSlots;

	public event Action<Item> OnItemRightClickedEvent;

    public ItemTooltip itemTooltip;

    public bool dragging = false;
    public ItemSlot draggingSlot;
    public ItemSlot hoveringSlot;
    public bool hoveringInventory;

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

    public void SetCurrency(Currency currency)
    {
        this.currency = currency;
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

	public bool RemoveItem(Item item, int quantity = 1)
	{
        if (item.Stackable)
        {
            foreach (var itemSlot in itemSlots)
            {
                if (itemSlot.Item == item)
                {
                    if (itemSlot.ItemQuantity > 1)
                    {
                        itemSlot.ItemQuantity = Mathf.Clamp(itemSlot.ItemQuantity - quantity, 0, 999);
                        return true;
                    }
                    else
                    {
                        itemSlot.Item = null;
                        return true;
                    }
                }
                
            }
        }
        else if (items.Remove(item))
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
    
    public bool HasItem(Item item, int quantity = 1)
    {
        foreach (var itemSlot in itemSlots)
        {
            if (itemSlot.Item == item)
            {
                if(item.Stackable)
                {
                    if (itemSlot.ItemQuantity >= quantity)
                    {
                        return true;
                    }
                }
                else
                {
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

    public void OnPointerExit(PointerEventData eventData)
    {
        hoveringInventory = false;
        print("cursor left inventory");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoveringInventory = true;
        print("cursor in inventory");
    }

    public void DropItem(ItemSlot itemSlot)
    {
        PlayableCharacter player = FindObjectOfType<PlayableCharacter>();
        Vector3 dropPos = new Vector3(player.transform.position.x, player.transform.position.y + 0.1f, player.transform.position.z);
        Item item = itemSlot.Item;
        if (RemoveItem(itemSlot.Item, itemSlot.ItemQuantity))
        {
            PickUpItem pickUpItem = Resources.Load<PickUpItem>("Prefabs/Items/PickUp/PickupItem");
            PickUpItem itemDropped = Instantiate(pickUpItem, dropPos, Quaternion.identity) as PickUpItem;
            itemDropped.GiveForce(0, 3);

            itemDropped.item = item;
            itemDropped.quantity = itemSlot.ItemQuantity;
        }
    }
}