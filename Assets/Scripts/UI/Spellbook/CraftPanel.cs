using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftPanel : MonoBehaviour {

    public TextMeshProUGUI title;
    private Ability ability;
    private Transform recipeDisplaysParent;
    public RecipeItemDisplay recipeItem;

    public void Initialize(Ability ability)
    {
        this.ability = ability;
        recipeDisplaysParent = transform.Find("RecipeItemDisplays");
        
        SetValues();
    }

    void SetValues()
    {
        title.text = "Craft - " + ability.Name;
        foreach (var item in ability.Recipe.recipeItems)
        {
            RecipeItemDisplay rid = Instantiate(recipeItem, recipeDisplaysParent) as RecipeItemDisplay;
            rid.SetRecipeItem(item);
        }
    }

    public void Craft()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayableCharacter>().LearnAbility(ability);
        Destroy(gameObject);
    }
}
