using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public enum Mode
    {
        normal,
        vertical
    }
    public Mode mode;
    public bool setDistance;
    public float distance;
    private void Start()
    {
        Set();
        //transform.position += (Camera.main.transform.position - transform.position).normalized * 3.0f;
    }

    void LateUpdate()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        switch (mode)
        {
            case Mode.normal:
                transform.forward = cameraForward;
                break;
            case Mode.vertical:
                transform.forward = new Vector3(transform.forward.x, cameraForward.y, transform.forward.z);
                break;

        }
    }

    public void Set()
    {
        if(setDistance)
        {
            Plane canvasPlane = new Plane(
                Camera.main.transform.forward,
                Camera.main.transform.position + (Camera.main.transform.forward.normalized * distance));
            Ray rayToCanvas = new Ray(transform.position, Camera.main.transform.position - transform.position);
            if (canvasPlane.Raycast(rayToCanvas, out float dist))
            {
                transform.position = rayToCanvas.origin + rayToCanvas.direction * dist;
            }
        }
    }
}
