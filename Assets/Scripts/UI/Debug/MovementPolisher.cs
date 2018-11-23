using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MovementPolisher : MonoBehaviour {
    
    private PlayerMovement playerMovement;
    public Slider baseMoveSpeed;
    public Slider maxMoveSpeed;
    public Slider jumpHeight;
    public Slider jumpDistance;
    public Slider acceleration;
    
    
    // Use this for initialization
    void Start () {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerMovement>();
        Initialize();
	}

    void Initialize()
    {
        baseMoveSpeed.value = playerMovement.baseMoveSpeed;
        maxMoveSpeed.value = playerMovement.maxMoveSpeed;
        jumpHeight.value = playerMovement.jumpforce.y;
        jumpDistance.value = playerMovement.jumpforce.x;
        acceleration.value = playerMovement.acceleration;
    }
	
    void Update() { 
        playerMovement.baseMoveSpeed = baseMoveSpeed.value;
        playerMovement.maxMoveSpeed = maxMoveSpeed.value;
        playerMovement.jumpforce.y = jumpHeight.value;
        playerMovement.jumpforce.x = jumpDistance.value;
        playerMovement.acceleration = acceleration.value;

        SetTextValues();
    }

    void SetTextValues()
    {
        baseMoveSpeed.transform.parent.Find("Value").GetComponent<TextMeshProUGUI>().text = baseMoveSpeed.value.ToString("F1");
        maxMoveSpeed.transform.parent.Find("Value").GetComponent<TextMeshProUGUI>().text = maxMoveSpeed.value.ToString("F1");
        jumpHeight.transform.parent.Find("Value").GetComponent<TextMeshProUGUI>().text = jumpHeight.value.ToString("F1");
        jumpDistance.transform.parent.Find("Value").GetComponent<TextMeshProUGUI>().text = jumpDistance.value.ToString("F1");
        acceleration.transform.parent.Find("Value").GetComponent<TextMeshProUGUI>().text = acceleration.value.ToString("F1");
    }
	
}
