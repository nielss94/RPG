using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class MapController : MonoBehaviour {

    [SerializeField] private List<MonsterSpawn> platforms = new List<MonsterSpawn>();
    
	void Awake () {
        SceneManager.sceneLoaded += SetMonsterPlatforms;
	}
    
    void SetMonsterPlatforms(Scene scene, LoadSceneMode mode)
    {
        platforms.Clear();
        platforms = FindObjectsOfType<MonsterSpawn>().ToList();
        if(platforms.Count > 0)
        {
            foreach (var platform in platforms)
            {
                platform.Initialize();
            }
        }
        InvokeRepeating("SpawnAvailableMonsters", 2, 10);
    }
    
    void SpawnAvailableMonsters()
    {
        foreach (var platform in platforms)
        {
            int activeMobs = platform.GetActiveMonsters();
            if(activeMobs < platform.maxMonsters)
            {
                platform.SpawnMonsters(platform.maxMonsters - activeMobs);
            }
        }
    }
}
