using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinateProjectile : MonoBehaviour {
    
    private ICanDealDamage owner;

    [SerializeField]
    private Damage damage;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float range;
    [SerializeField]
    private Transform player;

    private Vector2 tempTarget;
    private Transform realTarget;
    private bool approaching = false;
    
    public void Initialize(Vector2 tempTarget, Transform target, ICanDealDamage owner, Damage damage, Transform player)
    {
        this.target = target;
        this.owner = owner;
        this.damage = damage;
        this.tempTarget = tempTarget;
        this.player = player;
        Invoke("SetRealTarget", 2f);
    }

    void SetRealTarget()
    {
        if (target != null)
        {
            realTarget = target;
        }
        approaching = true;
    }

    void FixedUpdate () {
        transform.Rotate(0, 0, Random.Range(-1, 1) <= 0 ? -30 : 30);

        if (realTarget != null)
        {
            if (Vector2.Distance(transform.position,target.transform.position) < 0.1f)
            {
                owner.DealDamage(target.GetComponent<IDamageable>(), damage);

                //TODO: Object pooling?
                Destroy(gameObject);
            }else
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.fixedDeltaTime);  
            }
        }else
        {
            if (approaching)
            {
                if (Vector2.Distance(transform.position, player.position) > 0.1)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else if (Vector2.Distance(transform.position, tempTarget) > 0.1)
            {
                transform.position = Vector2.MoveTowards(transform.position, tempTarget, 4 * Time.fixedDeltaTime);
            }
        }
	}

    public float Range
    {
        get
        {
            return range;
        }

        set
        {
            range = value;
        }
    }
}
