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
                            character.playerPanel.UpdateDisplayValues();
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
        if (Input.GetKeyDown(KeyCode.T))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("Teleport"));

            if (hit.collider != null)
            {
                MapTeleport mt = hit.transform.GetComponent<MapTeleport>();
                StartCoroutine(MapController.Teleport(mt));
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("PickUp"));

            if(hit.collider != null)
            {
                hit.transform.GetComponent<PickUp>().Take(character);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            character.AddCurrency(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            character.AddCurrency(1000);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            character.AddCurrency(100000);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            character.AddCurrency(10000000);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            character.TakeCurrency(1000);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            character.TakeCurrency(100);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            character.TakeCurrency(10);
        }

        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            character.inventory.AddItem(Resources.Load<Item>("Prefabs/Items/The X"),5);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            character.inventory.AddItem(Resources.Load<Item>("Prefabs/Items/Recipes/Recipe for Assassinate"), 5);
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
