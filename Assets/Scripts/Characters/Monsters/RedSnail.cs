using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSnail : Monster
{
    new void Update()
    {
        base.Update();

        if (!IsDead)
        {
            if (Target != null)
            {
                ApproachTarget();
            }
            else
            {
                Idle();
            }
        }
    }

    public override IEnumerator AnimateAndDie()
    {
        IsDead = true;
        animator.SetBool("Dead", IsDead);
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        Destroy(gameObject);
    }
}
