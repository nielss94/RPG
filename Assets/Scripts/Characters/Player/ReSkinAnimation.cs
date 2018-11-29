using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReSkinAnimation : MonoBehaviour {

    public string Name;
    private new SpriteRenderer renderer;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if(Name != "")
        {
            var subSprites = Resources.LoadAll<Sprite>("Sprites/Equipment/"+ Name);
            string currentSpriteName = renderer.sprite.name;
            var newSprite = Array.Find(subSprites, item => item.name == currentSpriteName);

            if (newSprite)
                renderer.sprite = newSprite;
        }
    }

    public void SetSheet(string name)
    {
        Name = name;
    }
}
