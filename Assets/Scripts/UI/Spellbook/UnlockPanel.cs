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
        PlayableCharacter player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayableCharacter>();
        if (player.inventory.RemoveItem(ability.Recipe))
        {
            player.UnlockAbility(ability);
            Destroy(gameObject);
        }
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
