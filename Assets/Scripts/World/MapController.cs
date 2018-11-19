using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class MapController : MonoBehaviour {

    public static MapController Instance;

    [SerializeField] private List<MonsterSpawn> platforms = new List<MonsterSpawn>();

    public TeleportInfo previousTeleport;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Initialize(Scene scene, LoadSceneMode mode)
    {
        SetMonsterPlatforms();
        SetMapSong();
        if(previousTeleport != null)
        {
            if(previousTeleport.moveToIndex > 0)
                SpawnPlayer();
        }
    }

    public void SetMapSong()
    {
        AudioClip audioClip = FindObjectOfType<MapInfo>().mapSong;
        AudioSource musicPlayer = GetComponent<AudioSource>();
        if(audioClip != null)
        {
            if(audioClip != musicPlayer.clip)
            {
                musicPlayer.clip = audioClip;
                musicPlayer.Play();
            }
        }
        else
        {
            musicPlayer.Stop();
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
        player.GetComponent<PlayerMovement>().CanMove = true;

        player.transform.position = GetTeleportByIndex(previousTeleport.moveToIndex).transform.position;
    }

    public IEnumerator Teleport(MapTeleport teleport)
    {
        if(teleport.moveToIndex > 0)
        {
            previousTeleport = new TeleportInfo
            {
                moveToIndex = teleport.moveToIndex
            };
            CanvasHolder canvas = FindObjectOfType<CanvasHolder>();
            canvas.FadeScreen.Fade();
            FindObjectOfType<PlayableCharacter>().PlayerMovement.CanMove = false;
            yield return new WaitForSeconds(1f);
            canvas.MinimapPanel.SetCurrentMap(teleport.MoveTo);
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

