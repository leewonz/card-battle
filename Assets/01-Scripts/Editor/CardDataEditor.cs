using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardData), true)]
public class CardDataEditor : Editor
{
    SerializedProperty cardEffect;

    void OnEnable()
    {
        cardEffect = serializedObject.FindProperty("cardEffect");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        //serializedObject.Update();
        ////EditorGUILayout.ObjectField(cardEffect, typeof(AbstractCardEffect));
        //cardEffect.objectReferenceValue = 
        //    EditorGUILayout.ObjectField("Script:", cardEffect.objectReferenceValue, typeof(AbstractCardEffect), false);
        //serializedObject.ApplyModifiedProperties();
    }
}
