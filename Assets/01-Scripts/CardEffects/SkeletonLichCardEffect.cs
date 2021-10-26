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
        // �ߵ����� ���� ���� üũ
        if (card.cardState != Card.CardState.Summoned ||
            e.team != card.Slot.team)
        {
            return;
        }

        List<CharacterSlot> slots = Field.Instance.GetList(card.Slot.team);

        for (int i = 0; i < slots.Count; i++)
        {
            if(!slots[i].cardObject) // �˻��� ���Կ� ī�尡 ������
            {
                GameObject card = CardBuilder.Instance.CreateCard("mercSkeletonMinion");

                if(!card)
                {
                    Debug.LogError("��ȯ�� ĳ���� ī�带 ������ �� ����");
                    break;
                }

                slots[i].SummonCharacter(card);
                break;
            }
        }
    }
}
