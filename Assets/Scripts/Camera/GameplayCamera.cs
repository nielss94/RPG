using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayCamera : MonoBehaviour {

    private static GameObject player;
    public float speed;

    public static void Initialize(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if(player != null)
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z), speed * Time.fixedDeltaTime);
    }
}
