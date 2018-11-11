using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour {

    public bool fadeIn;
    Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (fadeIn)
        {
            if (image.color.a < 1)
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + 2 * Time.deltaTime);
        }
        if(!fadeIn)
        {
            if (image.color.a > 0)
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - 2 * Time.deltaTime);
            else
                gameObject.SetActive(false);
        }
    }

    public void Fade()
    {
        gameObject.SetActive(true);
        StartCoroutine(DoFade());
    }
    
    public IEnumerator DoFade()
    {
        fadeIn = true;

        yield return new WaitForSeconds(1f);

        fadeIn = false;
    }
}
