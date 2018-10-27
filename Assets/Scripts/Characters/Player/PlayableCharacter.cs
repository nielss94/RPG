using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(CharacterStats))]
public class PlayableCharacter : Character, ICanDealDamage, IDamageable {
    
    private PlayerMovement playerMovement;
    private CharacterStats stats;
    
    [Space]
    [Header("Abilities")]
    public AbilitySlot[] abilitySlots = new AbilitySlot[4];
    [SerializeField]
    private List<Ability> knownAbilities = new List<Ability>();


    public List<Ability> activeBuffs = new List<Ability>();

    [Space]
    [Header("UI")]
    public StatPanel statPanel;
    public EquipmentPanel equipmentPanel;
    public Inventory inventory;
    public BuffPanel buffPanel;
    public AbilitiesPanel abilitiesPanel;
    
    [Header("Combat")]
    [SerializeField]
    private float knockBackFrequency;
    private float knockBackTimer;
    [SerializeField]
    private float onHitMoveBlockTime;
    [HideInInspector]
    public float onHitMoveBlockTimer;
    [SerializeField]
    public float generalAbilityCooldown;
    [HideInInspector]
    public float generalAbilityCooldownTimer;


    void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
        stats = GetComponent<CharacterStats>();

        statPanel.SetStats(stats.PhysicalAttack, stats.MagicalAttack, stats.PhysicalDefense, stats.MagicalDefense,
                            stats.Strength, stats.Intelligence, stats.Vitality, stats.Agility);
        statPanel.UpdateStatValues();

        abilitiesPanel.SetAbilities(knownAbilities);
        abilitiesPanel.UpdateAbilityCardValues();

        
        inventory.OnItemRightClickedEvent += EquipFromInventory;
        equipmentPanel.OnItemRightClickedEvent += UnequipFromEquipPanel;
        statPanel.OnStatsChanged += OnStatsChanged;
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

        if(generalAbilityCooldownTimer > 0)
        {
            generalAbilityCooldownTimer -= Time.deltaTime;
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

    public void LearnAbility(Ability ability)
    {
        Ability a = knownAbilities.FirstOrDefault(s => s.Name == ability.Name);
        if(a == null)
        {
            knownAbilities.Add(ability);
            abilitiesPanel.UpdateAbilityCardValues();
        }
    }
    
    void OnStatsChanged()
    {
        Health.MaxHealth = (short)(Health.BaseMaxHealth + (Stats.Vitality.Value * 5));
    }

    public void GainExperience(int experience)
    {
        Experience += experience;
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

    /**
     * Equipment methods
     * */
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

    /**
     * Calculates damage and takes stats based on stats
     * */
    public Damage CalculateDamage()
    {
        float phys = Stats.PhysicalAttack.Value * (1 + (Stats.Strength.Value / 100f));
        float magical = Stats.MagicalAttack.Value * (1 + (Stats.Intelligence.Value / 100f));

        phys = Random.Range(phys - (phys / 100 * 3), phys + (phys / 100 * 3));
        magical = Random.Range(magical - (magical / 100 * 3), magical + (magical / 100 * 3));

        return new Damage((int)magical, (int)phys);
    }


    public void DealDamage(IDamageable damageable, Damage damage)
    {
        damageable.TakeDamage(damage, this);
    }

    public void TakeDamage(Damage damage, Character source)
    {
        //TODO: Apply armour
        Monster monster = (Monster)source;
        KnockBack(monster.Direction);
        Health.CurHealth = (short)Mathf.Clamp(Health.CurHealth - (damage.PhysicalAttack + damage.MagicalAttack), 0, Health.MaxHealth);
        //Debug.LogFormat("OUCH! {0} took {1} damage", Name, (damage.PhysicalAttack + damage.MagicalAttack));

        StartCoroutine(FloatingTextController.CreateDamageText(damage, new Vector2(transform.position.x, transform.position.y + 0.5f), true));

        if (Health.CurHealth <= 0)
        {
            Die();
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (Application.isPlaying)
        {
            if (Weapon != null)
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
                }
                else
                {
                    if (Weapon.WeaponType == WeaponTypes.Ranged)
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
