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

    public override void ShowOnHitEffect(Transform target)
    {
        throw new System.NotImplementedException();
    }

    public override void ShowOnSelfEffect()
    {
        throw new System.NotImplementedException();
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
