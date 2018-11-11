using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController Instance;

	void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            CanvasHolder canvasHolder = Resources.Load<CanvasHolder>("Prefabs/UI/Canvas");
            CanvasHolder canvas = Instantiate(canvasHolder) as CanvasHolder;
            canvas.name = "Canvas";
            canvas.MinimapPanel.SetCurrentMap(SceneManager.GetActiveScene().name);
            PlayableCharacter player = Resources.Load<PlayableCharacter>("Prefabs/Player");
            player.gameObject.name = player.Name;
            Instantiate(player);

            UIController.Initialize();
            SceneManager.sceneLoaded += GetComponent<MapController>().Initialize;
            SceneManager.sceneLoaded += GameplayCamera.Initialize;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
