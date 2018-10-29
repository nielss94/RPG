using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterSpawn : MonoBehaviour {

    public List<Monster> monsterTypes = new List<Monster>();
    public List<Monster> activeMonsters = new List<Monster>();
    public int maxMonsters;

    private float spawnXLeft;
    private float spawnXRight;

    private Transform buddy;
    

    public void Initialize()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 10, LayerMask.GetMask("MonsterSpawn"));
        
        if (hit.collider != null)
            buddy = hit.transform;
        else
            Destroy(gameObject);

        spawnXLeft = transform.position.x;
        spawnXRight = buddy.position.x;
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
