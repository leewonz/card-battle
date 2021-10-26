using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovController : MonoBehaviour
{
    public static readonly float baseFov = 40;
    public static readonly float focusFov = 36;

    public float changeRate;
    public float targetFov;
    List<Camera> cameras;

    private void Awake()
    {
        cameras = new List<Camera>();
        //cameras.AddRange(GetComponents<Camera>());
        cameras.AddRange(GetComponentsInChildren<Camera>());
        targetFov = cameras[0].fieldOfView;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for(int i = 0; i < cameras.Count; i++)
        {
            cameras[i].fieldOfView = Mathf.Lerp(cameras[i].fieldOfView, targetFov, changeRate);
        }
    }
}
