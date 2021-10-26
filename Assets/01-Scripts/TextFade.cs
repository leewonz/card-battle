using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour, IPoolObject
{
    public float preDelay;
    public float fadeTime;

    float preDelayCurr = 1;
    float alpha = 1;

    TextMeshProUGUI text;

    public void OnDespawn()
    {

    }

    public void OnSpawn()
    {
        preDelayCurr = preDelay;
        alpha = 1;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
    }

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeTime <= 0.0f) { fadeTime = 0.001f; }
        if (preDelay <= 0.0f) { preDelay = 0.001f; }

        if(preDelayCurr > 0) 
        {
            preDelayCurr -= Time.deltaTime; 
        }
        else 
        {
            alpha -= (1 / fadeTime) * Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        }
    }
}
