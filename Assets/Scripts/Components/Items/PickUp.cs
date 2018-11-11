using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class PickUp : MonoBehaviour {

    public bool taken;
    public PlayableCharacter character;
    private SpriteRenderer spriteRenderer;
    public OutlineObject outlineObject;

    public void Start()
    {
        outlineObject = GetComponent<OutlineObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void GiveForce(float x, float y)
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y), ForceMode2D.Impulse);
    }

    void Update()
    {
        if (taken)
        {
            if(spriteRenderer.color.a > 0)
            {
                transform.position = Vector2.Lerp(transform.position, character.transform.position, 10 * Time.deltaTime);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Clamp01(spriteRenderer.color.a - 0.01f));
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    public abstract void Take(PlayableCharacter character);
}
