using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperBody : Ability {

    [Space]
    [Header("Unique properties")]
    public int value;
    public StatModType type;
    

	public override void Execute(Character character) {
        Character = character as PlayableCharacter;
        ShowOnSelfEffect();
        Ability existingAbility = Character.AddBuff(this);
        if (existingAbility != null)
        {
            existingAbility.Duration = Duration;
            Destroy(gameObject);
        }else
        {
            Character.Stats.Vitality.AddModifier(new StatModifier(value, type, this));
            Character.statPanel.UpdateStatValues();
        }
    }

    public override void ShowOnHitEffect(Transform target)
    {
        throw new System.NotImplementedException();
    }

    public override void ShowOnSelfEffect()
    {
        ParticleSystem ps = Instantiate(OnSelf, Character.transform.position, Quaternion.identity, Character.transform);
        Destroy(ps.gameObject, ps.main.duration);
    }

    void Update()
    {
        if(Duration > 0)
        {
            Duration -= Time.deltaTime;
        }else
        {
            Character.Stats.Vitality.RemoveAllModifiersFromSource(this);
            Character.statPanel.UpdateStatValues();
            Character.RemoveBuff(this);
            Destroy(gameObject);
        }
    }
}
