using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageEffect : MonoBehaviour
{
    public bool useScreenShader;
    public bool useScreenSize;
    public bool clear;
    public Material mat;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (clear)
        {
            RenderTexture rt = UnityEngine.RenderTexture.active;
            UnityEngine.RenderTexture.active = destination;
            GL.Clear(true, true, Color.clear);
            UnityEngine.RenderTexture.active = rt;
        }

        if (useScreenShader)
        {
            if (useScreenSize)
            {
                mat.SetFloat("_TexSizeX", Screen.width);
                mat.SetFloat("_TexSizeY", Screen.height);
            }
            Graphics.Blit(source, destination, mat);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    public void Start()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }
}
