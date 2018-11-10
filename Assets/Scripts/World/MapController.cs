using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class MapController : MonoBehaviour {

    [SerializeField] private List<MonsterSpawn> platforms = new List<MonsterSpawn>();

    public static TeleportInfo previousTeleport;
    
    public void Initialize(Scene scene, LoadSceneMode mode)
    {
        SetMonsterPlatforms();
        if(previousTeleport != null)
        {
            if(previousTeleport.moveToIndex > 0)
                SpawnPlayer();
        }
    }
    
    void SetMonsterPlatforms()
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

    void SpawnPlayer()
    {
        PlayableCharacter player = FindObjectOfType<PlayableCharacter>();
        player.GetComponent<PlayerMovement>().IsJumping = true;
        player.GetComponent<PlayerMovement>().OnFloor = false;

        player.transform.position = GetTeleportByIndex(previousTeleport.moveToIndex).transform.position;
    }

    public static void Teleport(MapTeleport teleport)
    {
        if(teleport.moveToIndex > 0)
        {
            previousTeleport = new TeleportInfo
            {
                moveToIndex = teleport.moveToIndex
            };
            SceneManager.LoadScene(teleport.MoveTo);
        }
    }

    MapTeleport GetTeleportByIndex(int index)
    {
        foreach (var item in FindObjectsOfType<MapTeleport>())
        {
            if(item.teleportIndex == index)
            {
                return item;
            }
        }
        return null;
    }
}

