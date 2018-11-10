using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTeleport : MonoBehaviour {

    public int teleportIndex;

    public string MoveTo;
    public int moveToIndex;
}


public class TeleportInfo
{
    public int moveToIndex;
}
