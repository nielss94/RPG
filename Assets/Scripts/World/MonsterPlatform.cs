using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPlatform : MonoBehaviour {

    public List<Monster> monsterTypes = new List<Monster>();
    public List<Monster> activeMonsters = new List<Monster>();
    public int maxMonsters;

    private float spawnXLeft;
    private float spawnXRight;
    

    public void Initialize()
    {
        spawnXLeft = transform.position.x - (transform.localScale.x / 2);
        spawnXRight = transform.position.x + (transform.localScale.x / 2);
    }
	
	public void SpawnMonsters(int amount)
    {
        if(monsterTypes.Count > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                if (GetActiveMonsters() >= maxMonsters)
                    break;
                int mobIndex = 0;
                if (monsterTypes.Count > 1)
                {
                    mobIndex = (int)Mathf.Clamp(Random.Range(0, monsterTypes.Count),0, monsterTypes.Count - 0.1f);
                }

                float xPos = Random.Range(spawnXLeft, spawnXRight);
                Monster monster = Instantiate(monsterTypes[mobIndex], new Vector2(xPos, transform.position.y + (monsterTypes[mobIndex].transform.localScale.y)), Quaternion.identity) as Monster;
                activeMonsters.Add(monster);
            }
        }
    }
    
    public int GetActiveMonsters()
    {
        if(activeMonsters.Count > 0)
        {
            for (int i = activeMonsters.Count -1; i >= 0; i--)
            {
                if (activeMonsters[i] == null)
                    activeMonsters.Remove(activeMonsters[i]);
            }
        }
        return activeMonsters.Count;
    }
}
