using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(CharacterStats))]
public class PlayableCharacter : Character, ICanDealDamage, IDamageable {
    
    private PlayerMovement playerMovement;
    private CharacterStats stats;
    
    [Space]
    [Header("Abilities")]
    public AbilitySlot[] abilitySlots = new AbilitySlot[4];

    public List<Ability> activeBuffs = new List<Ability>();

    [Space]
    [Header("UI")]
    public StatPanel statPanel;
    public EquipmentPanel equipmentPanel;
    public Inventory inventory;
    public BuffPanel buffPanel;
    
    [Header("Combat")]
    [SerializeField]
    private float knockBackFrequency;
    private float knockBackTimer;
    [SerializeField]
    private float onHitMoveBlockTime;
    [HideInInspector]
    public float onHitMoveBlockTimer;


    void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
        stats = GetComponent<CharacterStats>();

        statPanel.SetStats(stats.PhysicalAttack, stats.MagicalAttack, stats.PhysicalDefense, stats.MagicalDefense,
                            stats.Strength, stats.Intelligence, stats.Vitality, stats.Agility);
        statPanel.UpdateStatValues();
        
        inventory.OnItemRightClickedEvent += EquipFromInventory;
        equipmentPanel.OnItemRightClickedEvent += UnequipFromEquipPanel;
    }

    void Update()
    {
        if(Weapon != null)
        {
            switch (Weapon.WeaponType)
            {
                case WeaponTypes.Melee:
                    abilitySlots[0].ability = Resources.Load<Ability>("Prefabs/Abilities/MeleeBasicAttack");
                    break;
                case WeaponTypes.Ranged:
                    abilitySlots[0].ability = Resources.Load<Ability>("Prefabs/Abilities/RangedBasicAttack");
                    break;
            }
        }

        foreach (AbilitySlot abilitySlot in abilitySlots)
        {
            if (abilitySlot.cooldownLeft > 0)
            {
                abilitySlot.cooldownLeft -= Time.deltaTime;
            }
        }

        if(knockBackTimer > 0)
        {
            knockBackTimer -= Time.deltaTime;
        }
        if(onHitMoveBlockTimer > 0)
        {
            onHitMoveBlockTimer -= Time.deltaTime;
        }
    }
    
    public void DealDamage(IDamageable damageable, Damage damage)
    { 
        damageable.TakeDamage(damage, this);
    }

    public void TakeDamage(Damage damage, Character source)
    {
        Monster monster = (Monster)source;
        KnockBack(monster.Direction);
        Health = (short)Mathf.Clamp(Health - (damage.PhysicalAttack + damage.MagicalAttack), 0, MaxHealth);
        Debug.LogFormat("OUCH! {0} took {1} damage", Name, (damage.PhysicalAttack + damage.MagicalAttack));

        StartCoroutine(FloatingTextController.CreateDamageText(damage, transform.position,true));

        if (Health <= 0)
        {
            Die();
        }
    }


    public Ability AddBuff(Ability ability)
    {
        foreach (var item in activeBuffs)
        {
            if(item.Name == ability.Name)
            {
                return item;
            }
        }
        buffPanel.AddBuff(ability);
        activeBuffs.Add(ability);
        return null;
    }

    public void RemoveBuff(Ability ability)
    {
        buffPanel.RemoveBuff(ability);
        activeBuffs.Remove(ability);
    }

    private void EquipFromInventory(Item item)
    {
        if (item is EquippableItem)
        {
            Equip((EquippableItem)item);
        }
    }

    private void UnequipFromEquipPanel(Item item)
    {
        if (item is EquippableItem)
        {
            Unequip((EquippableItem)item);
        }
    }

    public void Equip(EquippableItem item)
    {
        if (inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if (equipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }

   public void Unequip(EquippableItem item)
    {
        if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }

    public Damage CalculateDamage()
    {
        float phys = Stats.PhysicalAttack.Value * (1 + (Stats.Strength.Value / 100f));
        float magical = Stats.MagicalAttack.Value * (1 + (Stats.Intelligence.Value / 100f));

        phys = Random.Range(phys - (phys / 100 * 3), phys + (phys / 100 * 3));
        magical = Random.Range(magical - (magical / 100 * 3), magical + (magical / 100 * 3));

        return new Damage((int)magical, (int)phys);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
       
        if(Application.isPlaying)
        {
            if(Weapon != null)
            {
                Vector2 lineUp = new Vector2(transform.position.x, transform.position.y + 0.25f);
                Vector2 lineDown = new Vector2(transform.position.x, transform.position.y - 0.25f);
                float rangedOffset = 0.75f;
                if (playerMovement.IsOnFloor())
                {
                    Gizmos.DrawLine(lineDown,
                        GetComponent<PlayerMovement>().GetComponent<SpriteRenderer>().flipX ?
                        lineDown + (Vector2.left * Weapon.Range) :
                        lineDown + (Vector2.right * Weapon.Range));
                    Gizmos.DrawLine(lineUp,
                        GetComponent<PlayerMovement>().GetComponent<SpriteRenderer>().flipX ?
                        lineUp + (Vector2.left * Weapon.Range) :
                        lineUp + (Vector2.right * Weapon.Range));
                }else
                {
                    if(Weapon.WeaponType == WeaponTypes.Ranged)
                    {
                        lineUp = new Vector2(playerMovement.GetAimingDirection() == Vector2.left
                                                ? transform.position.x - rangedOffset
                                                : transform.position.x + rangedOffset, transform.position.y + rangedOffset);
                        lineDown = new Vector2(playerMovement.GetAimingDirection() == Vector2.left
                                                ? transform.position.x - rangedOffset
                                                : transform.position.x + rangedOffset, transform.position.y - rangedOffset);
                    }
                    Gizmos.DrawLine(lineDown,
                        GetComponent<PlayerMovement>().GetComponent<SpriteRenderer>().flipX ?
                        lineDown + (Vector2.left * Weapon.Range) :
                        lineDown + (Vector2.right * Weapon.Range));
                    Gizmos.DrawLine(lineUp,
                        GetComponent<PlayerMovement>().GetComponent<SpriteRenderer>().flipX ?
                        lineUp + (Vector2.left * Weapon.Range) :
                        lineUp + (Vector2.right * Weapon.Range));
                }
            }
            
        }
    }

    
    public void Die()
    {
        print(Name + " died.");
    }

    public void KnockBack(Vector2 direction)
    {
        if (knockBackTimer <= 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x * 2, 1), ForceMode2D.Impulse);
            knockBackTimer = knockBackFrequency;
            onHitMoveBlockTimer = onHitMoveBlockTime;
        }
    }

    public CharacterStats Stats
    {
        get
        {
            return stats;
        }

        set
        {
            stats = value;
        }
    }

    public Weapon Weapon
    {
        get
        {
            return equipmentPanel.GetWeapon();
        }
    }

    public PlayerMovement PlayerMovement
    {
        get
        {
            return playerMovement;
        }

        set
        {
            playerMovement = value;
        }
    }
}
