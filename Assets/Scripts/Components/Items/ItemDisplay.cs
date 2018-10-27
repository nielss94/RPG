using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDisplay : MonoBehaviour {

    public Image ItemImage;
    public TextMeshProUGUI ItemName;

    private void OnValidate()
    {
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        Image image = GetComponentInChildren<Image>();

        ItemImage = image;
        ItemName = text;
    }

    public void SetItem(Item item)
    {
        ItemImage.sprite = item.Icon;
        ItemName.text = item.Name;
    }
}
