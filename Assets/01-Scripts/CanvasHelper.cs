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

    //���� �ȼ� ���� ������ ���� �ػ󵵷� ������ �ȼ� ���� ��ȯ�Ѵ�.
    public Vector2 ToReferenceScale(Vector2 from)
    {
        //print(canvas.pixelRect + "\n"
        //    + (from / canvas.scaleFactor) + "\n" 
        //    + (from - new Vector2(canvas.pixelRect.width * 0.5f, canvas.pixelRect.height * 0.5f)) / canvas.scaleFactor);
        return (from - new Vector2(canvas.pixelRect.width * 0.5f, canvas.pixelRect.height * 0.5f)) / canvas.scaleFactor;
    }

    //���� �ȼ� ���� ������ ���� �ػ󵵷� ������ �ȼ� ���� ��ȯ�Ѵ�.
    public Rect ToReferenceScale(Rect from)
    {
        //print(canvas.pixelRect + "\n"
        //    + (from / canvas.scaleFactor) + "\n" 
        //    + (from - new Vector2(canvas.pixelRect.width * 0.5f, canvas.pixelRect.height * 0.5f)) / canvas.scaleFactor);
        return new Rect(ToReferenceScale(from.position), ToReferenceScale(from.size));
    }
}
