using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardData", order = 3)]
public class CardData : ScriptableObject
{
    public enum CardEffectType { Direct, One, All }

    [Serializable]
    public enum CardType
    {
        Merc,
        Skill
    }
    public Texture2D image;
    public Sprite sprite;
    public string codeName;
    public CardType cardType;

    public CardEffectType cardEffectType;
    public Team cardEffectTarget;

    public string characterCodename;
    public int manaCost;
    public int attackPoint;
    public int attackCount;
    public int healthPoint;

    public bool isAdvance;
    public bool isExhaust;

    public string nameEn;
    public string nameKo;
    [TextArea(4, 4)]
    public string descEn;
    [TextArea(4, 4)]
    public string descKo;

    //[SerializeReference]
    //public BaseCardEffect cardEffect;

    //[ContextMenu("set Class A")]
    //public void SetDrawCard()
    //{
    //    //cardEffect = new DrawCardEffect();
    //}

    //[ContextMenu("set Class B")]
    //public void SetEnergizeCard()
    //{
    //    //cardEffect = new EnergizeCardEffect();
    //}
}
