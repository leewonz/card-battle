using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Pile : MonoBehaviour
{
    public RectTransform scrollRect;

    [SerializeField]
    List<GameObject> cards;
    public List<GameObject> Cards
    {
        get { return cards; }
    }
    public int Count
    {
        get { return cards.Count; }
    }

    private void Awake()
    {
    }

    public GameObject Draw()
    {
        if (cards.Count > 0)
        {
            GameObject card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
        else
        {
            return null;
        }
    }

    public void Shuffle()
    {
        for(int i = cards.Count; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i);
            GameObject card = cards[randomIndex];
            cards.RemoveAt(randomIndex);
            cards.Add(card);
        }
    }

    public void AddNew(string codename)
    {
        GameObject createdCard = 
            CardBuilder.Instance.CreateCard(codename, (scrollRect) ? scrollRect : transform);

        if (createdCard.TryGetComponent<CardHandMovement>(out var cardHandMovement))
        {
            cardHandMovement.enabled = false;
        }

        createdCard.transform.SetParent((scrollRect) ? scrollRect : transform);
        createdCard.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, -1);
        createdCard.GetComponent<RectTransform>().localScale = Vector3.one;
        createdCard.GetComponent<RectTransform>().localRotation = Quaternion.identity;

        cards.Add(createdCard);
    }

    public void Add(GameObject cardObj)
    {

        if (cardObj.TryGetComponent<CardHandMovement>(out var cardHandMovement))
        {
            cardHandMovement.enabled = false;
        }

        cardObj.transform.SetParent((scrollRect) ? scrollRect : transform);
        cardObj.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, -1);
        cardObj.GetComponent<RectTransform>().localScale = Vector3.one;
        cardObj.GetComponent<RectTransform>().localRotation = Quaternion.identity;

        cards.Add(cardObj);
    }

    public void Clear()
    {
        for (int i = cards.Count - 1; i >= 0; i--)
        {
            Destroy(cards[i]);
        }
        cards.Clear();
    }
}
