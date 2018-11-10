using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Items/Potion")]
public class Potion : UsableItem
{
    [Header("Potion effects")]
    public int healthGain;
    public int manaGain;
    public int maxHealthGainPercentage;
    public int maxManaGainPercentage;
    
    public override void Use(PlayableCharacter character)
    {
        character.Health.CurHealth = (short)Mathf.Clamp(character.Health.CurHealth + healthGain, 0, character.Health.MaxHealth);
        character.Mana.CurMana = (short)Mathf.Clamp(character.Mana.CurMana + manaGain, 0, character.Mana.MaxMana);

        int manaGainPercToAmount = (int)(character.Mana.MaxMana / 100f * maxManaGainPercentage);
        character.Mana.CurMana = (short)Mathf.Clamp(character.Mana.CurMana + manaGainPercToAmount, 0, character.Mana.MaxMana);

        int healthGainPercToAmount = (int)(character.Health.MaxHealth / 100f * maxHealthGainPercentage);
        Debug.Log(healthGainPercToAmount);
        character.Health.CurHealth = (short)Mathf.Clamp(character.Health.CurHealth + healthGainPercToAmount, 0, character.Health.MaxHealth);

        ItemTooltip.Instance.HideTooltip();

        character.inventory.RemoveItem(this);
    }
}
