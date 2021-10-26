using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField]
    bool isPlayable = true;
    public bool IsPlayable { get { return isPlayable; } set { isPlayable = value; } }
    public int maxCardCount = 8;

    [SerializeField]
    List<GameObject> cards;

    Vector2 cardPosParent = new Vector2(0f, -1000f);
    float cardIdxDistance = 800f;
    float cardIdxRotation = 4.5f;

    public GameObject this[int i]
    {
        get { return this.cards[i]; }
    }

    public int Count
    {
        get { return cards.Count; }
    }

    private void Awake()
    {
        EventManager.Instance.DrawCardEvent += OnDrawCard;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Add(GameObject cardObj)
    {
        Card card = cardObj.GetComponent<Card>();
        if (card)
        {
            card.Draw();
            cards.Add(cardObj);
            cardObj.SetActive(true);
            cardObj.GetComponent<CardHandMovement>().enabled = true;
            cardObj.transform.SetParent(transform);
            //cardObj.transform.SetSiblingIndex(0);

            for (int i = 0; i < cards.Count; i++)
            {
                SetCardTargetPosition(i);
            }
        }
        else
        {
            Debug.LogError("Hand.cs : 드로우한 카드에 Card 컴포넌트가 없음");
        }

    }

    public GameObject Remove(int i)
    {
        GameObject cardObj = cards[i];
        cards.RemoveAt(i);
        return cardObj;
    }

    public bool Remove(GameObject cardObj)
    {
        if (cards.Remove(cardObj))
        {
            for (int i = 0; i < cards.Count; i++)
            {
                SetCardTargetPosition(i);
            }
            return true;
        }
        return false;
    }

    public int Find(GameObject cardObj)
    {
        if(cardObj.GetComponent<Card>())
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (cardObj == cards[i])
                {
                    return i;
                }
            }
            Debug.LogWarning("Hand.Find 함수에서 카드 번호를 찾을 수 없음");
            return -1;
        }
        Debug.LogWarning("Hand.Find 함수에서 주어진 카드의 Card 컴포넌트가 없음");
        return -1;
    }

    void SetCardTargetPosition(int cardIdx)
    {
        CardHandMovement cardMovement = cards[cardIdx].GetComponent<CardHandMovement>();
        float targetAngle = (cardIdx - ((cards.Count - 1) * 0.5f)) * cardIdxRotation;

        cardMovement.hand = this;

        Quaternion rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward * -1);
        cardMovement.targetPos = cardPosParent + (Vector2)(rotation * new Vector3(0f, cardIdxDistance, 0f));
        cardMovement.targetAngle = targetAngle;
        cardMovement.order = cardIdx;
    }

    public void Clear()
    {
        for (int i = cards.Count - 1; i >= 0; i--)
        {
            Destroy(cards[i]);
        }
        cards.Clear();
    }

    public void OnDrawCard<T>(object sender, T args) where T : DrawCardEventArgs
    {
        //print("Hand : OnDrawCard : " + args.DrawnCard.name);
    }
}
