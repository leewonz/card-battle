using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHelper : MonoBehaviour
{
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    //실제 픽셀 값을 받으면 기준 해상도로 맞춰진 픽셀 값을 반환한다.
    public Vector2 ToReferenceScale(Vector2 from)
    {
        //print(canvas.pixelRect + "\n"
        //    + (from / canvas.scaleFactor) + "\n" 
        //    + (from - new Vector2(canvas.pixelRect.width * 0.5f, canvas.pixelRect.height * 0.5f)) / canvas.scaleFactor);
        return (from - new Vector2(canvas.pixelRect.width * 0.5f, canvas.pixelRect.height * 0.5f)) / canvas.scaleFactor;
    }

    //실제 픽셀 값을 받으면 기준 해상도로 맞춰진 픽셀 값을 반환한다.
    public Rect ToReferenceScale(Rect from)
    {
        //print(canvas.pixelRect + "\n"
        //    + (from / canvas.scaleFactor) + "\n" 
        //    + (from - new Vector2(canvas.pixelRect.width * 0.5f, canvas.pixelRect.height * 0.5f)) / canvas.scaleFactor);
        return new Rect(ToReferenceScale(from.position), ToReferenceScale(from.size));
    }
}
