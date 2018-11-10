using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCurrency : PickUp
{
    public ulong value;

    new void Start()
    {
        base.Start();
        if (value > 10000)
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Items/Currency/money-bag");
        else if (value > 1000)
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Items/Currency/money-bag");
        else if (value > 100)
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Items/Currency/money-stack");
        else
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/Items/Currency/small-money");
    }

    public override void Take(PlayableCharacter character)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(transform.GetChild(0).gameObject);
        this.character = character;
        character.AddCurrency(value);
        taken = true;
    }
}
