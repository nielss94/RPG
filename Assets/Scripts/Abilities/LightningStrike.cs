using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : Ability {

    private PlayerMovement playerMovement;
    public int maxTargets;

    public override void Execute(Character character)
    {
        playerMovement = character.GetComponent<PlayerMovement>();
        Character = character as PlayableCharacter;
        if(IsCorrectWeaponType())
        {
            Attack(AcquireTarget());
        } else
        {
            Destroy(gameObject);
        }
    }

    public void Attack(List<RaycastHit2D> hit)
    {
        //TODO: Play attack animation

        if(hit.Count > 0)
        {
            if(hit.Count > maxTargets)
            {
                hit = hit.GetRange(0, maxTargets);
            }
            for (int i = 0; i < hit.Count; i++)
            {
                if (hit[i].collider != null)
                {
                    IDamageable damageable = hit[i].transform.GetComponent<IDamageable>();
                    ShowOnHitEffect(hit[i].transform);
                    StartCoroutine(PerformAttack(damageable));
                }
            }
        }else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator PerformAttack(IDamageable damageable)
    {
        for (int i = 0; i < NumberOfHits; i++)
        {
            if((Monster)damageable != null)
            {
                Character.DealDamage(damageable, CalculateDamage());
                yield return new WaitForSeconds(.1f);

            }
        }

        Destroy(gameObject);
    }

    public List<RaycastHit2D> AcquireTarget()
    {
        Vector2 startPos = Character.transform.position;
        Vector2 boxSize = new Vector2(0.1f, 0.5f);

        if (playerMovement.IsOnFloor())
        {
            return new List<RaycastHit2D>(Physics2D.BoxCastAll(startPos,
                                    boxSize,
                                    0,
                                    playerMovement.GetAimingDirection(),
                                    Range,
                                    LayerMask.GetMask("Monster")));
        }
        else
        {
            Debug.Log("Can't be casted in midair");

            return new List<RaycastHit2D>();
        }
    }
    
    Damage CalculateDamage()
    {
        Damage d = Character.CalculateDamage();
        d.PhysicalAttack = (int)(d.PhysicalAttack * (Power / 100f));
        d.MagicalAttack = (int)(d.MagicalAttack * (Power / 100f));
        return d;
    }
    
}
