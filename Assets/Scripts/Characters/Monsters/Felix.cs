using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Felix : Monster {
    
    
    new void Update () {
        base.Update();

        if(Target != null)
        {
            ApproachTarget();
        }else
        {
            Idle();
        }
    }

    public override IEnumerator AnimateAndDie()
    {
        Destroy(gameObject);
        yield return new WaitForSeconds(0);
    }
}
