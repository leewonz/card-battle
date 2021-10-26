using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardCardEffect : BaseCardEffect
{
    public override void Play(CharacterSlot slot)
    {
        EventManager.Instance.PlayCardEvent += OnPlayCard;
    }

    public override void Unsummon()
    {
        EventManager.Instance.PlayCardEvent -= OnPlayCard;
    }

    private void OnPlayCard(object sender, PlayCardEventArgs e)
    {
        if(card.cardState == Card.CardState.Summoned)
        {
            CardData playedCardData = e.PlayedCard.GetComponent<Card>().CardData;
            if (playedCardData.cardType == CardData.CardType.Skill)
            {
                Card.GetComponent<Character>().DoAttackSequence();
                if (GameObject.FindGameObjectWithTag(Con.Tags.cameraParent).
                    TryGetComponent<CameraTransformController>(out var transformController))
                {
                    CommandManager.Instance.AddLast(
                        transformController.GetTransformCommand(CameraTransformHolder.CameraTransformType.Normal, 0));
                }
            }
        }
    }
}
