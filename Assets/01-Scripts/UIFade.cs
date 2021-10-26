using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIFade : MonoBehaviour
{
    public float preDelay;
    public float fadeTime;

    float preDelayCurr = 1;
    float alpha = 1;

    float originalImageAlpha = -1;
    float originalTextAlpha = -1;

    public Image image;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(alpha > 0)
        {
            if (fadeTime <= 0.0f) { fadeTime = 0.001f; }
            if (preDelay <= 0.0f) { preDelay = 0.001f; }

            if (preDelayCurr > 0)
            {
                preDelayCurr -= Time.deltaTime;
            }
            else
            {
                alpha -= (1 / fadeTime) * Time.deltaTime;

                if (image)
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, alpha * originalImageAlpha);
                }
                if (text)
                {
                    text.color = new Color(text.color.r, text.color.g, text.color.b, alpha * originalTextAlpha);
                }
            }
        }
        else
        {
            image.enabled = false;
            text.enabled = false;
        }
    }

    public void StartFade()
    {
        preDelayCurr = preDelay;
        alpha = 1;

        image.enabled = true;
        text.enabled = true;

        if(originalImageAlpha < 0)
        {
            originalImageAlpha = image.color.a;
        }

        if (originalTextAlpha < 0)
        {
            originalTextAlpha = text.color.a;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, originalImageAlpha);
        text.color = new Color(text.color.r, text.color.g, text.color.b, originalTextAlpha);
    }
}
