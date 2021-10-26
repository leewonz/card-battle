using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCards : MonoBehaviour
{
    public CardList startDeck;

    public Pile deckPile;
    public Pile drawPile;
    public Hand hand;
    public Pile discardPile;
    public Pile exhaustedPile;

    private void Awake()
    {
        EventManager.Instance.DrawCardEvent += OnDrawCard;
        EventManager.Instance.PlayCardEvent += OnPlayCard;
        EventManager.Instance.DiscardCardEvent += OnDiscardCard;
    }

    // Start is called before the first frame update
    void Start()
    {
        //EventManager.Instance.DrawCardEvent += OnDrawCard;
    }

    //private void OnDrawCard(GameObject cardObj)
    //{
    //    throw new System.NotImplementedException();
    //}

    public void InitGame()
    {
        for (int i = 0; i < startDeck.GetDataCount(); i++)
        {
            deckPile.AddNew(startDeck.GetData(i).codeName);
        }
    }

    public void InitBattle()
    {
        List<GameObject> originalCards = deckPile.Cards;
        int originalCardsCount = originalCards.Count;

        drawPile.Clear();
        hand.Clear();
        discardPile.Clear();

        for(int i = 0; i < originalCardsCount; i++)
        {
            GameObject newlyCopiedObject = CardBuilder.Instance.CopyFrom(originalCards[i], drawPile.gameObject.transform);
            if(newlyCopiedObject.TryGetComponent<Card>(out var newCard))
            {
                if(newCard.CardData.isAdvance)
                {
                    hand.Add(newlyCopiedObject);
                }
                else
                {
                    drawPile.Add(newlyCopiedObject);
                }
            }
        }
        drawPile.Shuffle();
    }

    public void StartTurn()
    {
        if(TryGetComponent<TeamController>(out var playerController))
        {
            Draw(playerController.drawCountPerTurn);
            hand.IsPlayable = true;
        }
        else
        {
            Debug.LogError("PlayerCards.cs - StartTurn 턴 시작시 플레이어 컨트롤러를 찾을 수 없음");
        }
    }

    public void EndTurn()
    {
        hand.IsPlayable = false;
    }

    public int Draw(int count)
    {
        int drawCount = 0;
        for(int i = 0; i < count; i++)
        {
            if(hand.Count >= hand.maxCardCount)
            {
                return drawCount;
            }

            if(drawPile.Count == 0)
            {
                Reshuffle();
            }

            GameObject cardObj = drawPile.Draw();

            if (cardObj)
            {
                hand.Add(cardObj);
                drawCount++;
                EventManager.Instance.Invoke(EventManager.GameEventType.DrawCard, new DrawCardEventArgs(cardObj));
            }
            else
            {
                Debug.LogWarning("PlayerCards.Draw 함수 : 더미에 아무 것도 없어서 못 뽑았다.");
            }
        }
        return drawCount;
    }

    public GameObject DiscardFromHand(int handIdx)
    {
        GameObject cardObj = hand[handIdx];
        if(cardObj)
        {
            hand.Remove(cardObj);
            discardPile.Add(cardObj);
            //cardObj.SetActive(false);
            EventManager.Instance.Invoke(EventManager.GameEventType.DiscardCard, new DiscardCardEventArgs(cardObj));
        }

        return cardObj;
    }

    public GameObject DiscardFromHand(GameObject cardObj)
    {
        if (cardObj)
        {
            hand.Remove(cardObj);
            discardPile.Add(cardObj);
            //cardObj.SetActive(false);
            cardObj.GetComponent<Card>().cardState = Card.CardState.InPile;
            EventManager.Instance.Invoke(EventManager.GameEventType.DiscardCard, new DiscardCardEventArgs(cardObj));
        }

        return cardObj;
    }

    public GameObject Exhaust(GameObject cardObj)
    {
        if (cardObj)
        {
            exhaustedPile.Add(cardObj);
            cardObj.SetActive(false);
            cardObj.GetComponent<Card>().cardState = Card.CardState.Exhausted;
            //EventManager.Instance.Invoke(EventManager.EventTypes.DiscardCard, new DiscardCardEventArgs(cardObj));
        }

        return cardObj;
    }

    public GameObject Summon(GameObject cardObj, CharacterSlot slot)
    {
        if (cardObj)
        {
            hand.Remove(cardObj);
            slot.SummonCharacter(cardObj);
            
            //EventManager.Instance.Invoke(EventManager.EventTypes.DiscardCard, new DiscardCardEventArgs(cardObj));
        }

        return cardObj;
    }

    public void Reshuffle()
    {
        while(discardPile.Count > 0)
        {
            drawPile.Add(discardPile.Draw());
        }
        drawPile.Shuffle();
    }

    public void OnDrawCard<T>(object sender, T args) where T : DrawCardEventArgs
    {
        //print("DrawCardEvent Raised : " + args.DrawnCard.name);
    }

    public void OnPlayCard<T>(object sender, T args) where T : PlayCardEventArgs
    {
        //GameObject card = args.PlayedCard;
        //int cardHandIdx = hand.Find(card);
        //if (cardHandIdx >= 0)
        //{
        //    DiscardFromHand(cardHandIdx);
        //}
    }

    public void OnDiscardCard<T>(object sender, T args) where T : DiscardCardEventArgs
    {
        print("DiscardCardEvent Raised : " + args.DiscardedCard.name);
    }
    
}
