using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : PickUp {

    public Item item;
    public int quantity;

    new void Start()
    {
        base.Start();
        GetComponent<SpriteRenderer>().sprite = item.Icon;
    }

    public override void Take(PlayableCharacter character)
    {
        this.character = character;
        if (character.inventory.AddItem(item, quantity))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(transform.GetChild(0).gameObject);
            taken = true;
        }
    }
}
