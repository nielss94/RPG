using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour {

    public Animator animator;
    
	void Start () {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
	}

    public void SetText(string text)
    {
        animator.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void SetColor(Color color)
    {
        animator.GetComponent<TextMeshProUGUI>().color = color;
    }

    public void SetColorGradient(TMP_ColorGradient color)
    {
        animator.GetComponent<TextMeshProUGUI>().colorGradientPreset = color;
    }
}
