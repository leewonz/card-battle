using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Con;

public class Card : MonoBehaviour
{
    public enum CardState
    {
        InHand,
        InPile,
        Summoned,
        Exhausted
    }

    [SerializeField]
    private CardData cardData;
    [SerializeField]
    private BaseCardEffect cardEffect;

    public CardState cardState = CardState.InHand;
    public GameObject modelPrefab;
    public CardAppearance cardAppearance;

    //public TextMeshPro titleText;
    //public TextMeshPro descText;

    [HideInInspector]
    public int manaCost = 0;

    private readonly string cardObjNamePrefix = "Card - ";

    GameObject modelInstance;
    Character character;
    CharacterSlot slot;

    //public SpriteRenderer CardArtRenderer;

    public CardData CardData
    {
        get
        {
            return cardData;
        }
        set
        {
            cardData = value;
            //CardArtRenderer.sprite = cardData.sprite;
            GetComponent<CardHandMovement>().cardData = cardData;
            gameObject.name = cardObjNamePrefix + cardData.name;
            manaCost = cardData.manaCost;
            cardAppearance.CardData = cardData;
        }
    }

    public BaseCardEffect CardEffect
    {
        get
        {
            return cardEffect;
        }
        set
        {
            cardEffect = value;
        }
    }

    public CharacterSlot Slot
    {
        get
        {
            return slot;
        }
        set
        {
            slot = value;
        }
    }

    public Character Character
    {
        get
        {
            return character;
        }
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(CharacterSlot slot)
    {
        if (cardState == CardState.InHand)
        {
            TeamController playerController = 
                GameObject.FindGameObjectWithTag(Tags.player).GetComponent<TeamController>();
            PlayerCards playerCards =
                GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerCards>();
            if (manaCost <= playerController.Mana)
            {
                switch(cardData.cardType)
                {
                    case CardData.CardType.Skill:
                        playerCards.DiscardFromHand(gameObject);

                        break;
                    case CardData.CardType.Merc:
                        playerCards.Summon(gameObject, slot);
                        break;
                }
                if(cardEffect != null)
                {
                    cardEffect.Play(slot);
                }
                EventManager.Instance.Invoke(EventManager.GameEventType.PlayCard, new PlayCardEventArgs(gameObject));
                playerController.Mana -= manaCost;
            }
        }
    }

    public void Draw()
    {
        cardState = CardState.InHand;
        cardEffect.Draw();
        EventManager.Instance.Invoke(EventManager.GameEventType.DrawCard, new DrawCardEventArgs(gameObject));
    }

    public void Summon(CharacterSlot slot)
    {
        Slot = slot;

        character = gameObject.AddComponent<Character>();
        character.Init(this, cardData);

        modelInstance = Instantiate(modelPrefab, slot.transform);
        character.Model = modelInstance;

        cardState = CardState.Summoned;
        cardEffect.Summon(slot);
        EventManager.Instance.Invoke(EventManager.GameEventType.Summon, new SummonEventArgs(gameObject, slot));
    }

    public void Unsummon(CharacterSlot.UnsummonType type)
    {
        PlayerCards playerCards;
        //Destroy(modelInstance);
        character = gameObject.GetComponent<Character>();
        
        if (character)
        {
            switch(type)
            {
                case CharacterSlot.UnsummonType.Die:
                    character.Die();
                    break;
                case CharacterSlot.UnsummonType.Replaced:
                    character.Replaced();
                    break;
                case CharacterSlot.UnsummonType.BySystem:
                    character.UnsummonBySystem();
                    break;
            }
            playerCards = DataManager.GetPlayerCards(Slot.team);
            if (playerCards)
            {
                playerCards.Exhaust(gameObject);
            }
            Destroy(character);
            character = null;
            Slot = null;
        }
        else
        {
            Debug.Log("소환해제 캐릭터가 없음", gameObject);
        }
    }


}
