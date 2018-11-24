﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nairo : Monster {
    
    new void Update()
    {
        base.Update();
        if (Target != null)
        {
            ApproachTarget();
        }
        else
        {
            Idle();
        }
    }

    public override IEnumerator AnimateAndDie()
    {
        yield return new WaitForSeconds(0);
        Destroy(gameObject);
    }
}
