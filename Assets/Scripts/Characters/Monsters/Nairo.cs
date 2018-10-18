using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nairo : Monster {
    
    [SerializeField]
    private bool onFloor;
    
    new void Update()
    {
        base.Update();
        Idle();
        if (onFloor)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            onFloor = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Platform"))
        {
            onFloor = true;
        }
    }
}
