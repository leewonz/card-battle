using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardList", menuName = "ScriptableObjects/CardList", order = 4)]
public class CardList : ScriptableObject
{
    public List<CardData> datas;

    public CardData GetData(int i)
    {
        return datas[i];
    }

    public int GetDataCount()
    {
        return datas.Count;
    }
}
