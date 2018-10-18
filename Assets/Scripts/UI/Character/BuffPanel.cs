using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPanel : MonoBehaviour {

    public List<BuffDisplay> buffDisplays = new List<BuffDisplay>();

    public void AddBuff(Ability ability)
    {
        BuffDisplay bd = Instantiate(Resources.Load<BuffDisplay>("Prefabs/UI/BuffDisplay"), transform) as BuffDisplay;
        bd.Initialize(ability);
        buffDisplays.Add(bd);
    }

    public void RemoveBuff(Ability ability)
    {
        foreach (var item in buffDisplays)
        {
            if(item.ability == ability)
            {
                buffDisplays.Remove(item);
                Destroy(item.gameObject);
                return;
            }
        }
    }
}
