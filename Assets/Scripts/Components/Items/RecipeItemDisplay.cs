using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeItemDisplay : MonoBehaviour {

    public Image ItemImage;
    public TextMeshProUGUI ItemAmount;
    public TextMeshProUGUI ItemName;

    private void OnValidate()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        Image image = GetComponentInChildren<Image>();

        ItemImage = image;
        ItemAmount = texts[0];
        ItemName = texts[1];
    }

    public void SetRecipeItem(RecipeItem item)
    {
        ItemImage.sprite = item.item.Icon;
        ItemAmount.text = item.amount.ToString() + "x";
        ItemName.text = item.item.Name;
    }
}
