using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    
    private PlayableCharacter character;
    
    void Awake () {
        character = GetComponent<PlayableCharacter>();
	}

    void Initialize()
    {
        //Setup controls here
    }
	
	void Update () {
        foreach (AbilitySlot abilitySlot in character.abilitySlots)
        {
            if (Input.GetKeyDown(abilitySlot.hotKey) && abilitySlot.ability != null)
            {
                if(character.generalAbilityCooldownTimer <= 0)
                {
                    if (abilitySlot.cooldownLeft <= 0)
                    {
                        if (character.Mana.CurMana >= abilitySlot.ability.Cost)
                        {
                            Ability a = Instantiate(abilitySlot.ability, transform.position, Quaternion.identity) as Ability;
                            a.Execute(character);

                            character.Mana.CurMana = (short)Mathf.Clamp(character.Mana.CurMana - abilitySlot.ability.Cost, 0, character.Mana.MaxMana);
                            abilitySlot.cooldownLeft = abilitySlot.ability.Cooldown;
                            character.generalAbilityCooldownTimer = character.generalAbilityCooldown;
                        }
                        else
                        {
                            //TODO: Send message to player: Not enough mana
                            print("Not enough mana to use this ability");
                        }
                    }else
                    {
                        //TODO: Send message to player: Ability on cooldown
                        print("This ability is on cooldown");
                    }
                } else
                {
                    print("Waiting for general ability cooldown first.");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            character.LearnAbility(Resources.Load<Ability>("Prefabs/Abilities/Assassinate"));
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            character.LearnAbility(Resources.Load<Ability>("Prefabs/Abilities/MeleeBasicAttack"));
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            character.spellbook.gameObject.SetActive(!character.spellbook.gameObject.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            character.equipmentPanel.gameObject.SetActive(!character.equipmentPanel.gameObject.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            character.statPanel.gameObject.SetActive(!character.statPanel.gameObject.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            character.inventory.gameObject.SetActive(!character.inventory.gameObject.activeSelf);
        }
    }
}
