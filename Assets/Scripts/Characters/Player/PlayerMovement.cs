using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    private SpriteRenderer spriteRenderer;
    
    [SerializeField]
    private bool onFloor;

    public float baseMoveSpeed;
    [SerializeField]
    private float moveSpeed;
    public float maxMoveSpeed;

    public Vector2 jumpforce;
    public float acceleration;

    public float move;
    public float jump;


    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveSpeed = baseMoveSpeed;
	}

    void FixedUpdate()
    {
        HandleInput();
    }

    public void HandleInput()
    {
        move = Input.GetAxis("Walk");
        jump = Input.GetAxis("Jump");

        spriteRenderer.flipX = move > 0 ? false : (move < 0 ? true : spriteRenderer.flipX);
        if (move != 0 && onFloor && GetComponent<Rigidbody2D>().velocity.y <= 0 && GetComponent<PlayableCharacter>().onHitMoveBlockTimer <= 0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            moveSpeed = Mathf.Lerp(moveSpeed, maxMoveSpeed, acceleration * Time.fixedDeltaTime);
            transform.Translate(move * Time.fixedDeltaTime * moveSpeed, 0, 0);
        }
        else if(move == 0 && onFloor && GetComponent<PlayableCharacter>().onHitMoveBlockTimer <= 0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            moveSpeed = Mathf.Lerp(moveSpeed, baseMoveSpeed, 4 * Time.fixedDeltaTime);
        }
        if (move != 0 && !onFloor)
        {
            transform.Translate(move * Time.fixedDeltaTime * (baseMoveSpeed / 4), 0, 0);
        }
        if (jump > 0 && onFloor && GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            if(move > 0)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(jump * jumpforce.x, jumpforce.y), ForceMode2D.Impulse);
            }
            else if (move < 0)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-jump * jumpforce.x, jumpforce.y), ForceMode2D.Impulse);
            }
            else
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpforce.y), ForceMode2D.Impulse);
            }
            onFloor = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Platform") && GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            onFloor = true;
        }
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Platform"))
        {
            onFloor = false;
        }
    }

    public bool IsOnFloor()
    {
        return onFloor;
    }

    public Vector2 GetAimingDirection()
    {
        return spriteRenderer.flipX ? Vector2.left : Vector2.right;
    }

}
