using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Felix : Monster {
    
    
	// Update is called once per frame
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
    
}
