using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Items/Recipe")]
public class Recipe : UsableItem {

    public List<RecipeItem> recipeItems = new List<RecipeItem>();
}
