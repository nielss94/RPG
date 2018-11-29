using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [SerializeField] private SpriteRenderer standardRenderer;
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer legsRenderer;
    [SerializeField] private SpriteRenderer hatRenderer;

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

    Transform player;

    [SerializeField]private bool canMove;

    void Start () {
        player = transform.parent;

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

            standardRenderer.flipX = move > 0 ? false : (move < 0 ? true : standardRenderer.flipX);
            bodyRenderer.flipX = move > 0 ? false : (move < 0 ? true : bodyRenderer.flipX);
            legsRenderer.flipX = move > 0 ? false : (move < 0 ? true : legsRenderer.flipX);
            hatRenderer.flipX = move > 0 ? false : (move < 0 ? true : hatRenderer.flipX);
            if (move != 0 && onFloor && player.GetComponent<Rigidbody2D>().velocity.y <= 0 && player.GetComponent<PlayableCharacter>().onHitMoveBlockTimer <= 0)
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                moveSpeed = Mathf.Lerp(moveSpeed, maxMoveSpeed, acceleration * Time.fixedDeltaTime);
                player.Translate(move * Time.fixedDeltaTime * moveSpeed, 0, 0);
            }
            else if(move == 0 && onFloor && player.GetComponent<PlayableCharacter>().onHitMoveBlockTimer <= 0)
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                moveSpeed = Mathf.Lerp(moveSpeed, baseMoveSpeed, 4 * Time.fixedDeltaTime);
            }
            if (move != 0 && !onFloor)
            {
                if(!isJumping && player.GetComponent<Rigidbody2D>().velocity.y <= 0)
                {
                    player.Translate(move * Time.fixedDeltaTime * (moveSpeed / 2), 0, 0);
                }
                else
                {
                    player.Translate(move * Time.fixedDeltaTime * (baseMoveSpeed / 4), 0, 0);
                }
            }
            if (jump > 0 && onFloor && player.GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                if(move > 0)
                {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(jump * jumpforce.x, jumpforce.y), ForceMode2D.Impulse);
                }
                else if (move < 0)
                {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-jump * jumpforce.x, jumpforce.y), ForceMode2D.Impulse);
                }
                else
                {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpforce.y), ForceMode2D.Impulse);
                }
                onFloor = false;
                isJumping = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Platform") && player.GetComponent<Rigidbody2D>().velocity.y <= 0)
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

    void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Platform") && player.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            isJumping = false;
            onFloor = true;
        }
    }

    public bool IsOnFloor()
    {
        return onFloor;
    }

    public Vector2 GetAimingDirection()
    {
        return standardRenderer.flipX ? Vector2.left : Vector2.right;
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
