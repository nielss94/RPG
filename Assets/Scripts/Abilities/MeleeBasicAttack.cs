using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBasicAttack : Ability {

    private PlayerMovement playerMovement;

    public override void Execute(Character character)
    {
        playerMovement = character.GetComponentInChildren<PlayerMovement>();
        Character = character as PlayableCharacter;
        if(IsCorrectWeaponType())
        {
            Attack(AcquireTarget());
        }else
        {
            Destroy(gameObject);
        }
    }

    public void Attack(RaycastHit2D hit)
    {
        //TODO: Play attack animation

        if (hit.collider != null)
        {
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            ShowOnHitEffect(hit.transform);
            Character.DealDamage(damageable, Character.CalculateDamage());
        }

        Destroy(gameObject);
    }

    public RaycastHit2D AcquireTarget()
    {
        Vector2 startPos = Character.transform.position;
        Vector2 boxSize = new Vector2(0.1f, 0.5f);

        if (playerMovement.IsOnFloor())
        {
            return Physics2D.BoxCast(startPos,
                                    boxSize,
                                    0,
                                    playerMovement.GetAimingDirection(),
                                    Character.Weapon.Range,
                                    LayerMask.GetMask("Monster"));
        }
        else
        {
            return Physics2D.BoxCast(startPos,
                                    boxSize,
                                    0,
                                    playerMovement.GetAimingDirection(),
                                    Character.Weapon.Range,
                                    LayerMask.GetMask("Monster"));
        }
    }
    
}
