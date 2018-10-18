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
        //TODO: Get user's saved controls from db
        
    }
	
	void Update () {
        foreach (AbilitySlot abilitySlot in character.abilitySlots)
        {
            if (Input.GetKeyDown(abilitySlot.hotKey))
            {
                if(abilitySlot.cooldownLeft <= 0)
                {
                    Ability a = Instantiate(abilitySlot.ability, transform.position, Quaternion.identity) as Ability;
                    a.Execute(character);

                    abilitySlot.cooldownLeft = abilitySlot.ability.Cooldown;
                }
            }
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
