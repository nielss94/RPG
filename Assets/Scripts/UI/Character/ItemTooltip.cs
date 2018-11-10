using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ItemTooltip : MonoBehaviour
{
	public static ItemTooltip Instance;

	[SerializeField] Text nameText;
	[SerializeField] Text slotTypeText;
	[SerializeField] Text infoText;

	private StringBuilder sb = new StringBuilder();

	private void Awake()
	{
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy(this);
		}
		gameObject.SetActive(false);
	}

	public void ShowTooltip(Item itemToShow)
	{
        if (itemToShow is EquippableItem)
            ShowEquippableToolTip(itemToShow);
		else if(itemToShow is Recipe)
            ShowRecipeToolTip(itemToShow);
        else if(itemToShow is Potion)
            ShowPotionToolTip(itemToShow);
        else
            ShowEtcToolTip(itemToShow);
	}

    public void ShowRecipeToolTip(Item itemToShow)
    {
        Recipe recipe = (Recipe)itemToShow;

        gameObject.SetActive(true);

        nameText.text = recipe.Name;
        slotTypeText.text = "Recipe";

        infoText.text = recipe.Description;

    }

    public void ShowPotionToolTip(Item itemToShow)
    {
        Potion potion = (Potion)itemToShow;

        gameObject.SetActive(true);

        nameText.text = potion.Name;
        slotTypeText.text = "Potion";

        sb.Length = 0;

        if (potion.healthGain > 0)
            AddStatText(potion.healthGain, " Health");
        if (potion.manaGain > 0)
            AddStatText(potion.healthGain, " Mana");
        if (potion.maxHealthGainPercentage > 0)
            AddStatText(potion.healthGain, "% Health of max health");
        if (potion.maxManaGainPercentage > 0)
            AddStatText(potion.healthGain, " Mana of max mana");

        sb.AppendLine();

        sb.Append(potion.Description);

        infoText.text = sb.ToString();
    }

    public void ShowEtcToolTip(Item itemToShow)
    {
        gameObject.SetActive(true);

        nameText.text = itemToShow.Name;
        slotTypeText.text = "Etc.";

        infoText.text = itemToShow.Description;
    }

    public void ShowEquippableToolTip(Item itemToShow)
    {
        EquippableItem item = (EquippableItem)itemToShow;

        gameObject.SetActive(true);

        nameText.text = item.Name;
        slotTypeText.text = item.EquipmentType.ToString();

        sb.Length = 0;

        AddStatText(item.PhysicalAttack, " Physical attack");
        AddStatText(item.MagicalAttack, " Magical attack");
        AddStatText(item.PhysicalDefense, " Physical defense");
        AddStatText(item.MagicalDefense, " Magical defense");
        AddStatText(item.StrengthBonus, " Strength");
        AddStatText(item.AgilityBonus, " Agility");
        AddStatText(item.IntelligenceBonus, " Intelligence");
        AddStatText(item.VitalityBonus, " Vitality");

        AddStatText(item.StrengthPercentBonus * 100, "% Strength");
        AddStatText(item.AgilityPercentBonus * 100, "% Agility");
        AddStatText(item.IntelligencePercentBonus * 100, "% Intelligence");
        AddStatText(item.VitalityPercentBonus * 100, "% Vitality");

        infoText.text = sb.ToString();
    }

	public void HideTooltip()
	{
		gameObject.SetActive(false);
	}

	private void AddStatText(float statBonus, string statName)
	{
		if (statBonus != 0)
		{
			if (sb.Length > 0)
				sb.AppendLine();

			if (statBonus > 0)
				sb.Append("+");

			sb.Append(statBonus);
			sb.Append(statName);
		}
	}
}