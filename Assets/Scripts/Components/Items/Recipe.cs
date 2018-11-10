using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Items/Recipe")]
public class Recipe : UsableItem {

    public List<RecipeItem> recipeItems = new List<RecipeItem>();
    public Ability ability;

    public override void Use(PlayableCharacter character)
    {
        UIController.OpenUnlockPanel(ability);
        ItemTooltip.Instance.HideTooltip();
    }
}
