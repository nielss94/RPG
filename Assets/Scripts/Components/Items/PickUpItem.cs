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

    public override void Take(PlayableCharacter playableCharacter)
    {
        if (playableCharacter.inventory.AddItem(item, quantity))
        {
            Destroy(gameObject);
        }
    }
}
