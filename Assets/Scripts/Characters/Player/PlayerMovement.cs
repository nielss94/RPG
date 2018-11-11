using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private bool onFloor;
    [SerializeField]
    private bool isJumping = true;

    public float baseMoveSpeed;
    [SerializeField]
    private float moveSpeed;
    public float maxMoveSpeed;

    public Vector2 jumpforce;
    public float acceleration;

    public float move;
    public float jump;

    [SerializeField]private bool canMove;

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
        if (canMove)
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
                if(!isJumping && GetComponent<Rigidbody2D>().velocity.y <= 0)
                {
                    transform.Translate(move * Time.fixedDeltaTime * (moveSpeed / 2), 0, 0);
                }
                else
                {
                    transform.Translate(move * Time.fixedDeltaTime * (baseMoveSpeed / 4), 0, 0);
                }
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
                isJumping = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Platform") && GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            isJumping = false;
            onFloor = true;
        }
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
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

    public bool OnFloor
    {
        get
        {
            return onFloor;
        }

        set
        {
            onFloor = value;
        }
    }

    public bool IsJumping
    {
        get
        {
            return isJumping;
        }

        set
        {
            isJumping = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return canMove;
        }

        set
        {
            canMove = value;
        }
    }
}
