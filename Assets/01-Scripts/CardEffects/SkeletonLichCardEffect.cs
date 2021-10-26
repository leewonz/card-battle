using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonLichCardEffect : BaseCardEffect
{
    public override void Summon(CharacterSlot slot)
    {
        EventManager.Instance.StartTurnEvent += OnStartTurn;
    }

    public override void Unsummon()
    {
        EventManager.Instance.StartTurnEvent -= OnStartTurn;
    }

    private void OnStartTurn(object sender, StartTurnEventArgs e)
    {
        // 발동하지 않을 조건 체크
        if (card.cardState != Card.CardState.Summoned ||
            e.team != card.Slot.team)
        {
            return;
        }

        List<CharacterSlot> slots = Field.Instance.GetList(card.Slot.team);

        for (int i = 0; i < slots.Count; i++)
        {
            if(!slots[i].cardObject) // 검사한 슬롯에 카드가 없으면
            {
                GameObject card = CardBuilder.Instance.CreateCard("mercSkeletonMinion");

                if(!card)
                {
                    Debug.LogError("소환된 캐릭터 카드를 가져올 수 없음");
                    break;
                }

                slots[i].SummonCharacter(card);
                break;
            }
        }
    }
}
