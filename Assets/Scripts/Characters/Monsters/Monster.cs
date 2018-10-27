using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public abstract class Monster : Character, IDamageable {
    
    [SerializeField]
    private Weapon weapon;

    private Equipment equipment;

    private Dictionary<Character, List<int>> attackers = new Dictionary<Character, List<int>>();

    public int experienceReward;

    public event System.Action OnHealthChanged;

    private float timeSinceDamage;
    private int hitCount;

    [Header("Movement settings")]
    public bool moveRight = true;
    public bool jumpDuringApproach = true;
    [SerializeField] [Range(0,10)]private int jumpFrequency;
    [SerializeField] private bool onFloor;
    [SerializeField] private Transform platformChecker;

    [SerializeField] [Range(0.5f, 10)] private float minJumpCooldown = 3f;
    [SerializeField] [Range(0.5f, 10)] private float maxJumpCooldown = 3f;
    private float jumpTimer;

    [HideInInspector] public Vector2 destination;
    private Vector2 direction;

    [Header("Combat Settings")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float lookRange;
    [SerializeField]
    private float attackCooldown;
    private float attackTimer;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float knockBackFrequency;
    private float knockBackTimer;
    

    void Awake()
    {
        if (!equipment)
            equipment = GetComponent<Equipment>();
        attackTimer = attackCooldown;
        knockBackTimer = knockBackFrequency;

        float r = Random.Range(-1f, 1f);
        moveRight = r > 0 ? true : false;
        SetRotation();
    }

    public void Update()
    {
        if(timeSinceDamage > 0)
        {
            timeSinceDamage -= Time.deltaTime;
        }

        if(attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if (knockBackTimer > 0)
        {
            knockBackTimer -= Time.deltaTime;
        }

        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    public void Die()
    {
        //TODO: Drop loot
        foreach(var character in attackers)
        {
            int damageDealt = character.Value.Sum(d => d);
            double damagePercentage = Mathf.Clamp(damageDealt / Health.MaxHealth * 100f, 0,100);
            PlayableCharacter player = (PlayableCharacter)character.Key;
            player.GainExperience((int)(experienceReward / 100 * damagePercentage));
            Debug.LogFormat("{0} defeated {1}. He dealt {2}({3}%) damage!", character.Key.Name, Name, damageDealt.ToString(), damagePercentage.ToString());
        }
        //TODO: Object pooling?
        Destroy(gameObject);
    }

    public void TakeDamage(Damage damage, Character source)
    {
        if (Target != source.transform)
        {
            Target = source.transform;
        }

        PlayableCharacter player = (PlayableCharacter)source;
        KnockBack(player.PlayerMovement.GetAimingDirection());

        if (timeSinceDamage <= 0)
            hitCount = 0;
        
        int physArmor = 100 + equipment.GetTotalPhysicalDefense();
        float physMultiply = 100F / physArmor;
        damage.PhysicalAttack = (int)(damage.PhysicalAttack * physMultiply);

        int magArmor = 100 + equipment.GetTotalMagicalDefense();
        float magMultiply = 100F / magArmor;
        damage.MagicalAttack = (int)(damage.MagicalAttack * magMultiply);

        int lastHit = 0;
        if(damage.GetTotalDamage() >= Health.CurHealth)
        {
            lastHit = Health.CurHealth;
        }

        Health.CurHealth = (short)Mathf.Clamp(Health.CurHealth - (damage.PhysicalAttack + damage.MagicalAttack), 0, Health.MaxHealth);
        if (OnHealthChanged != null)
            OnHealthChanged();

        if(timeSinceDamage > 0)
        {
            hitCount++;
        }
        timeSinceDamage = 0.15f;
        StartCoroutine(FloatingTextController.CreateDamageText(damage, new Vector2(transform.position.x, transform.position.y + 0.5f + (.5f*hitCount))));
       
        //Debug.LogFormat("OUCH! {0} took {1} damage", Name, (damage.PhysicalAttack + damage.MagicalAttack));

        if (!attackers.ContainsKey(source))
        {
            attackers.Add(source, new List<int> { lastHit != 0 ? lastHit : damage.GetTotalDamage() });
        }
        else
        {
            attackers[source].Add(lastHit != 0 ? lastHit : damage.GetTotalDamage());
        }

        if (Health.CurHealth <= 0)
        {
            Die();
        }
    }

    public void KnockBack(Vector2 direction)
    {
        if(knockBackTimer <= 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x * 2, 1), ForceMode2D.Impulse);
            knockBackTimer = knockBackFrequency;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("Player") && attackTimer <= 0)
        {
            IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(weapon.GetDamage(this), this);
            }
            attackTimer = attackCooldown;
        }
    }

    public virtual void ApproachTarget()
    {
        if (Vector2.Distance(transform.position, Target.position) < LookRange)
        {
            destination.y = transform.position.y;
            if (Vector2.Distance(transform.position, Target.position) < attackRange)
            {
                if (Vector2.Distance(transform.position, destination) < 0.1f)
                {
                    destination = new Vector2(Random.Range(Target.position.x - attackRange, Target.position.x + attackRange), transform.position.y);
                }
            }
            else
            {
                destination = new Vector2(Target.position.x, transform.position.y);
            }
            if (destination.x > transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                direction = Vector2.right;
            }
            else
            {
                direction = Vector2.left;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, destination, MoveSpeed * Time.deltaTime);
            if (jumpDuringApproach) {
                HandleJump();
            }
        }
        else
        {
            LoseTarget();
        }

        //TODO: Jump when target is above but not too far
    }

    public virtual void Idle()
    {
        RaycastHit2D platformInfo = Physics2D.Raycast(platformChecker.GetChild(0).position, Vector2.down, 0.5f, LayerMask.GetMask("Platform"));
        if(platformInfo.collider == null)
        {
            ChangeRotation();
        }
        if (moveRight)
            transform.Translate(1 * Time.deltaTime, 0, 0);
        else
            transform.Translate(-1 * Time.deltaTime, 0, 0);

        HandleJump();
    }

    void HandleJump()
    {
        if(jumpFrequency > 0)
        {
            if (onFloor)
            {
                if(jumpTimer <= 0)
                {
                    int decider = Random.Range(1, 10);
                    if (decider <= jumpFrequency || jumpFrequency >= 10)
                    {
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
                        onFloor = false;
                    }

                    jumpTimer = Random.Range(minJumpCooldown, maxJumpCooldown);
                }
            }
        }
    }

    void SetRotation()
    {
        if (moveRight)
        {
            platformChecker.Rotate(0, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            platformChecker.Rotate(0, -180, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void ChangeRotation()
    {
        if (moveRight)
        {
            moveRight = false;
            platformChecker.Rotate(0, -180, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            moveRight = true;
            platformChecker.Rotate(0, 180, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            onFloor = true;
        }
    }

    public virtual void LoseTarget()
    {
        //Choose secondary target if there is another one.
        Target = null;
    }


    public Weapon GetWeapon()
    {
        return weapon;
    }

    public Equipment GetEquipment()
    {
        return equipment;
    }

    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }

        set
        {
            moveSpeed = value;
        }
    }

    public Transform Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    public float LookRange
    {
        get
        {
            return lookRange;
        }

        set
        {
            lookRange = value;
        }
    }

    public float AttackRange
    {
        get
        {
            return attackRange;
        }

        set
        {
            attackRange = value;
        }
    }

    public Vector2 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    public int JumpFrequency
    {
        get
        {
            return jumpFrequency;
        }

        set
        {
            jumpFrequency = value;
        }
    }

    public bool OnFloor
    {
        get
        {
            return onFloor;
        }

        set
        {
            onFloor = value;
        }
    }
}
