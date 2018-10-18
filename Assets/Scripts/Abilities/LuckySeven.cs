using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckySeven : Ability {
    
    RangedBasicAttack rangedBasicAttack;


    public override void Execute(Character character)
    {
        Character = character as PlayableCharacter;
        if(IsCorrectWeaponType())
        {
            rangedBasicAttack = Resources.Load<RangedBasicAttack>("Prefabs/Abilities/RangedBasicAttack");
            rangedBasicAttack.Initialize(Character);
            StartCoroutine(Fire());
        }else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Fire()
    {
        RaycastHit2D hit = rangedBasicAttack.AcquireTarget();
        rangedBasicAttack.Attack(hit);
        yield return new WaitForSeconds(0.1f);
        rangedBasicAttack.Attack(hit);
        Destroy(gameObject);
    }

}
