using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField] Image image;
    [SerializeField] TextMeshProUGUI quantityText;

	public event Action<Item> OnRightClickEvent;

    private Vector3 originalPos;

    [SerializeField] private Inventory inventory;

    [SerializeField]private int itemQuantity = 1;

	private Item _item;
	public Item Item {
		get { return _item; }
		set {
			_item = value;

			if (_item == null) {
				image.sprite = null;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
			} else {
				image.sprite = _item.Icon;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
                image.enabled = true;
			}
		}
	}
    public int ItemQuantity
    {
        get{ return itemQuantity; }
        set {
            itemQuantity = value;

            if (itemQuantity > 1 && quantityText != null)
                quantityText.text = itemQuantity.ToString();
            else
                quantityText.text = "";
        }
    }
    
    void Awake()
    {
        if(!(this is EquipmentSlot))
            inventory = transform.parent.GetComponentInParent<Inventory>();
    }

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (Item != null && OnRightClickEvent != null)
                OnRightClickEvent(Item);
		}
	}

	protected virtual void OnValidate()
	{
		if (image == null)
			image = GetComponent<Image>();
        if (quantityText == null)
            quantityText = GetComponentInChildren<TextMeshProUGUI>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
        if (inventory != null)
        {
            if (inventory.dragging)
            {
                if (inventory.draggingSlot != this)
                    inventory.hoveringSlot = this;
            }
            else
            {
                if(Item != null)
                {
                    ItemTooltip.Instance.ShowTooltip(Item); 
                }
            }
        }
        if(this is EquipmentSlot && Item != null)
        {
            ItemTooltip.Instance.ShowTooltip(Item);
        }
	}

	public void OnPointerExit(PointerEventData eventData)
	{
        if (inventory != null)
        {
            if (inventory.dragging)
            {
                inventory.hoveringSlot = null;
            }
        }
        if(Item != null)
        {
            ItemTooltip.Instance.HideTooltip();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (inventory != null)
        {
            if (inventory.dragging && Item != null)
            {
                GetComponent<CanvasGroup>().blocksRaycasts = false;
                image.rectTransform.position = eventData.position;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (inventory != null)
        {
            inventory.draggingSlot = this;
            originalPos = image.rectTransform.position;
            inventory.dragging = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (inventory != null)
        {
            if (inventory.dragging)
            {
                inventory.dragging = false;
                if (inventory.hoveringSlot != null )
                {
                    inventory.MoveItem();
                }
                else if(!inventory.hoveringInventory)
                {
                    inventory.DropItem(this);
                }
                image.rectTransform.position = originalPos;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }
    }

    
}