using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockPanel : MonoBehaviour {

    public TextMeshProUGUI title;
    public ItemDisplay recipeDisplay;
    private Ability ability;

    public void Initialize(Ability ability)
    {
        this.ability = ability;
        SetValues();
    }

    void SetValues()
    {
        title.text = "Unlock - " + ability.Name;
        recipeDisplay.SetItem(ability.Recipe);
    }

    public void Unlock()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayableCharacter>().LearnAbility(ability);
        Destroy(gameObject);
    }
}
