using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassinate : Ability {

    private PlayerMovement playerMovement;

    public override void Execute(Character character)
    {
        Initialize(character as PlayableCharacter);
        if (IsCorrectWeaponType())
        {
            Attack(AcquireTarget());

            Destroy(gameObject);
        }
    }

    public void Initialize(PlayableCharacter character)
    {
        Character = character;
        playerMovement = Character.GetComponent<PlayerMovement>();
    }

    public void Attack(RaycastHit2D hit)
    {
        //TODO: Play fitting attack animation

        //TODO: Get available projectile from inventory
        for (int i = 0; i < NumberOfHits; i++)
        {
            AssassinateProjectile steely = Resources.Load<AssassinateProjectile>("Prefabs/Equippables/Weapons/Projectiles/AssassinateProjectile");
            AssassinateProjectile projectile = Instantiate(steely, Character.transform.position, Quaternion.identity) as AssassinateProjectile;
            float x = Random.Range(transform.position.x - 1.5f, transform.position.x + 1.5f);
            float y = Random.Range(transform.position.y + (transform.localScale.y / 2), transform.position.y + 1.5f);
            projectile.Initialize(new Vector2(x,y), hit.transform, Character, Character.CalculateDamage(),Character.transform);
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
