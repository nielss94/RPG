using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ability : MonoBehaviour {

    [SerializeField]
    private new string name;
    [SerializeField]
    private Sprite image;
    [SerializeField]
    private float cooldown;
    [SerializeField]
    private short cost;
    [SerializeField]
    private float range;
    [SerializeField]
    private int numberOfHits = 1;
    [SerializeField]
    private float power;
    [SerializeField]
    private float duration;
    [SerializeField]
    private WeaponTypes weaponType;
    [SerializeField]
    private Recipe recipe;

    [Header("Effects")]
    [SerializeField]
    private ParticleSystem onSelf;
    [SerializeField]
    private ParticleSystem onCast;
    [SerializeField]
    private ParticleSystem onHit;


    private PlayableCharacter character;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public float Cooldown
    {
        get
        {
            return cooldown;
        }

        set
        {
            cooldown = value;
        }
    }
    
    public short Cost
    {
        get
        {
            return cost;
        }

        set
        {
            cost = value;
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

    public int NumberOfHits
    {
        get
        {
            return numberOfHits;
        }

        set
        {
            numberOfHits = value;
        }
    }

    public float Power
    {
        get
        {
            return power;
        }

        set
        {
            power = value;
        }
    }

    public WeaponTypes WeaponType
    {
        get
        {
            return weaponType;
        }

        set
        {
            weaponType = value;
        }
    }

    public PlayableCharacter Character
    {
        get
        {
            return character;
        }

        set
        {
            character = value;
        }
    }

    public ParticleSystem OnSelf
    {
        get
        {
            return onSelf;
        }

        set
        {
            onSelf = value;
        }
    }

    public ParticleSystem OnCast
    {
        get
        {
            return onCast;
        }

        set
        {
            onCast = value;
        }
    }

    public ParticleSystem OnHit
    {
        get
        {
            return onHit;
        }

        set
        {
            onHit = value;
        }
    }

    public Sprite Image
    {
        get
        {
            return image;
        }

        set
        {
            image = value;
        }
    }

    public float Duration
    {
        get
        {
            return duration;
        }

        set
        {
            duration = value;
        }
    }

    public Recipe Recipe
    {
        get
        {
            return recipe;
        }

        set
        {
            recipe = value;
        }
    }

    public abstract void Execute(Character character);
    
    public virtual void ShowOnHitEffect(Transform target)
    {
        if(OnHit != null)
        {
            float height = target.GetComponent<SpriteRenderer>().bounds.size.y;
            ParticleSystem ps = Instantiate(OnHit, new Vector2(target.position.x, target.position.y + (height /3)), Quaternion.identity, target) as ParticleSystem;
            Destroy(ps.gameObject, ps.main.duration);
        }
    }

    public virtual void ShowOnSelfEffect()
    {
        if(OnSelf != null)
        {
            ParticleSystem ps = Instantiate(OnSelf, Character.transform.position, Quaternion.identity, Character.transform);
            Destroy(ps.gameObject, ps.main.duration);
        }
    }

    public virtual bool IsCorrectWeaponType()
    {
        try
        {
            if (character.Weapon != null)
            {
                if (character.Weapon.WeaponType != WeaponType)
                {
                    throw new WeaponTypeException();
                }
            }
            else if (WeaponType != WeaponTypes.None)
            {
                throw new NoWeaponException();
            }
            return true;
        }
        catch (WeaponTypeException e)
        {
            e.LogAsWarning();
            return false;
        }
        catch (NoWeaponException e)
        {
            e.LogAsWarning();
            return false;
        }
    }
}
