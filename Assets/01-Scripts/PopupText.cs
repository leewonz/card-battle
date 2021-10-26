using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PopupText : MonoBehaviour
{
    public enum TextType
    {
        StartBattle,
        WinBattle,
        LoseBattle
    }

    [Serializable]
    public struct TextData
    {
        public TextType textType;
        public string stringEn;
        public string stringKo;
    }

    public List<TextData> textDatas;
    
    public void ShowText(TextType textType)
    {
        for(int i = 0; i < textDatas.Count; i++)
        {
            if(textDatas[i].textType == textType)
            {
                GetComponentInChildren<TextMeshProUGUI>().text = textDatas[i].stringKo;
                GetComponent<UIFade>().StartFade();
            }
        }
    }
}
