using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCardController : MonoBehaviour
{
    public CardList summonOnStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitBattle()
    {
        
        for(int i = 0; i < summonOnStart.datas.Count; i++)
        {
            GameObject card = CardBuilder.Instance.CreateCard(summonOnStart.datas[i].codeName);
            if(card)
            {
                Field.Instance.GetSlot(Team.Opponent, i).SummonCharacter(card);
            }
            else
            {
                Debug.LogError("적 캐릭터 카드를 가져올 수 없음");
            }
        }
    }
}
