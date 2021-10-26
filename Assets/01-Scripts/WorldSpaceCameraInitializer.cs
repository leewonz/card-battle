using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceCameraInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject cameraObject;
        if(cameraObject = GameObject.FindGameObjectWithTag(Con.Tags.uiCamera))
        {
            if (cameraObject.TryGetComponent<Camera>(out Camera camera))
            {
                GetComponent<Canvas>().worldCamera = camera;
            }
            else
            {
                Debug.LogWarning("Camera 없음");
            }
        }
        else
        {
            Debug.LogWarning("Camera 없음");
        }
    }
}
