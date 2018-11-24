using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUI : MonoBehaviour {

    private Monster monster;
    public Image health;

	void Awake () {
        monster = GetComponent<Monster>();
        monster.OnHealthChanged += OnHealthChanged;
	}
	
	void OnHealthChanged()
    {
        if (health.transform.parent.parent.gameObject.activeSelf == false)
            health.transform.parent.parent.gameObject.SetActive(true);
        health.transform.localScale = new Vector2((float)monster.Health.CurHealth / monster.Health.MaxHealth, health.transform.localScale.y);

        if(monster.Health.CurHealth <= 0)
        {
            health.transform.parent.parent.gameObject.SetActive(false);
        }
    }
}
