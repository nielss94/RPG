using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBasicAttack : Ability
{
    private PlayerMovement playerMovement;

    public override void Execute(Character character)
    {
        Initialize(character as PlayableCharacter);
        if(IsCorrectWeaponType())
        {
            Attack(AcquireTarget());

            Destroy(gameObject);
        }
    }

    public void Initialize(PlayableCharacter character)
    {
        Character = character;
        playerMovement = Character.PlayerMovement;
    }

    public void Attack(RaycastHit2D hit)
    {
        //TODO: Play attack animation

        //TODO: Get available projectile from inventory
        Projectile steely = Resources.Load<Projectile>("Prefabs/Equippables/Weapons/Projectiles/Steely");
        Projectile projectile = Instantiate(steely, Character.transform.position, Quaternion.identity) as Projectile;
        if (hit.collider != null)
        {
            projectile.Initialize(hit.transform, playerMovement.GetAimingDirection(), Character, Character.CalculateDamage());
        }
        else
        {
            projectile.Initialize(playerMovement.GetAimingDirection());
        }

    }   

    public RaycastHit2D AcquireTarget()
    {
        Vector2 startPos = Character.transform.position;
        Vector2 boxSize = new Vector2(0.1f, 0.5f);
        float rangedOffset = 0.75f;

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
            boxSize.y = 1.5f;
            startPos = new Vector2(playerMovement.GetAimingDirection() == Vector2.left
                                ? Character.transform.position.x - rangedOffset
                                : Character.transform.position.x + rangedOffset,
                                Character.transform.position.y);
            return Physics2D.BoxCast(startPos,
                                    boxSize,
                                    0,
                                    playerMovement.GetAimingDirection(),
                                    Character.Weapon.Range,
                                    LayerMask.GetMask("Monster"));
        }
    }
}
