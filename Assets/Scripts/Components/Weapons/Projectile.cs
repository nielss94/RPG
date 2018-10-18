using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    
    private ICanDealDamage owner;
    private Vector2 direction;
    private Vector2 maxReach;

    [SerializeField]
    private Damage damage;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float range;
    

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
        maxReach = new Vector2(direction.x > 0 ? transform.position.x + Range : transform.position.x - Range, transform.position.y);
    }

    public void Initialize(Transform target, Vector2 direction, ICanDealDamage owner, Damage damage)
    {
        this.target = target;
        this.direction = direction;
        this.owner = owner;
        this.damage = damage;
        maxReach = new Vector2(direction.x * (transform.position.x + Range), transform.position.y);
    }

    void FixedUpdate () {
        if (target != null)
        {
            if(Vector2.Distance(transform.position,target.transform.position) < 0.1f)
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
            if(Vector2.Distance(transform.position, maxReach) < 0.2f)
            {
                //TODO: Object pooling?
                Destroy(gameObject);
            }else
            {
                transform.Translate(direction.x * (speed * Time.fixedDeltaTime), 0, 0);
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
