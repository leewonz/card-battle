using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardAppearance : MonoBehaviour
{
    private readonly string cardTitleTextPrefix = "<font-weight=700>";
    private readonly string cardTitleTextSuffix = "</font-weight>";
    private readonly string cardDescTextPrefix = "<nobr>";
    private readonly string cardDescTextSuffix = "</nobr>";

    public GameObject mercAppearance;
    public GameObject skillAppearance;

    public Image image;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descText;
    public NumberTextMeshUI costNumberText;
    public NumberTextMeshUI attackNumberText;
    public NumberTextMeshUI healthNumberText;

    CardData cardData;
    public CardData CardData
    {
        get { return cardData; }
        set
        {
            cardData = value;
            image.sprite = cardData.sprite;
            titleText.text = cardTitleTextPrefix + cardData.nameKo + cardTitleTextSuffix;
            descText.text = cardDescTextPrefix + cardData.descKo + cardDescTextSuffix;
            costNumberText.FirstVal = cardData.manaCost;

            switch(cardData.cardType)
            {
                case CardData.CardType.Merc:
                    mercAppearance.SetActive(true);
                    skillAppearance.SetActive(false);
                    attackNumberText.FirstVal = cardData.attackPoint;
                    attackNumberText.SecondVal = cardData.attackCount;
                    healthNumberText.FirstVal = cardData.healthPoint;
                    break;
                case CardData.CardType.Skill:
                    mercAppearance.SetActive(false);
                    skillAppearance.SetActive(true);
                    break;
            }
        }
    }

    public void SetManaCost(int cost)
    {
        costNumberText.FirstVal = cost;
    }
}
