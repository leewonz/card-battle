using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class CardBuilder : MonoSingleton<CardBuilder>
{
    public delegate BaseCardEffect ReturnBaseCardEffect();

    public GameObject cardPrefab;
    public CardList cardList;
    [SerializeField]
    private Dictionary <string, CardData> cardDataList = new Dictionary<string, CardData>();
    [SerializeField]
    private Dictionary <string, ReturnBaseCardEffect> cardEffectList = new Dictionary<string, ReturnBaseCardEffect>();

    protected override void Init()
    {
        cardList = Resources.Load<CardList>("CardList");

        for(int i = 0; i < cardList.GetDataCount(); i++)
        {
            Debug.Log(i + "/" + cardList.GetDataCount());
            cardDataList.Add(cardList.GetData(i).codeName, cardList.GetData(i));
        }

        RegisterCardEffect("draw",                  BaseCardEffect.ReturnObject<DrawCardEffect>);
        RegisterCardEffect("energize",              BaseCardEffect.ReturnObject<EnergizeCardEffect>);
        RegisterCardEffect("massHeal",              BaseCardEffect.ReturnObject<MassHealCardEffect>);
        RegisterCardEffect("mercExplorer",          BaseCardEffect.ReturnObject<BaseCardEffect>);
        RegisterCardEffect("dualWield",             BaseCardEffect.ReturnObject<DualWieldCardEffect>);
        RegisterCardEffect("fireball",              BaseCardEffect.ReturnObject<FireballCardEffect>);
        RegisterCardEffect("attack",                BaseCardEffect.ReturnObject<AttackCardEffect>);
        RegisterCardEffect("mercViking",            BaseCardEffect.ReturnObject<BaseCardEffect>);
        RegisterCardEffect("mercOrcWarrior",        BaseCardEffect.ReturnObject<OrcWarriorCardEffect>);
        RegisterCardEffect("mercSkeletonLich",      BaseCardEffect.ReturnObject<SkeletonLichCardEffect>);
        RegisterCardEffect("mercSkeletonMinion",    BaseCardEffect.ReturnObject<BaseCardEffect>);
        RegisterCardEffect("mercWizard",            BaseCardEffect.ReturnObject<WizardCardEffect>);
        RegisterCardEffect("mercRockGolem",         BaseCardEffect.ReturnObject<RockGolemCardEffect>);
        RegisterCardEffect("mercEnemyViking",       BaseCardEffect.ReturnObject<BaseCardEffect>);
        RegisterCardEffect("mercEnemyOrcWarrior",   BaseCardEffect.ReturnObject<OrcWarriorCardEffect>);
        RegisterCardEffect("mercEnemyOrcVillager",  BaseCardEffect.ReturnObject<BaseCardEffect>);
        RegisterCardEffect("mercEnemySkeletonLich", BaseCardEffect.ReturnObject<SkeletonLichCardEffect>);
    }

    public void RegisterCardEffect(string codename, ReturnBaseCardEffect cardEffect)
    {
        if (cardDataList.ContainsKey(codename))
        {
            cardEffectList.Add(codename, cardEffect);
        }
        else 
        {
            Debug.LogWarning("카드 등록 시" + codename + "코드 네임이 데이터에 없음");
        }
    }

    public void RegisterCardEffect(string codename)
    {
        RegisterCardEffect(codename, BaseCardEffect.ReturnObject<BaseCardEffect>);
    }

    public GameObject CreateCard(string codename, Transform parent = null)
    {
        GameObject result = Object.Instantiate(cardPrefab);
        Card card = result.GetComponent<Card>();
        if(card)
        {
            if(cardDataList.TryGetValue(codename, out CardData cardData))
            {
                card.CardData = cardData;
            }
            else
            {
                Debug.LogWarning("카드 생성 시" + codename + "코드 네임이 데이터에 없음");
            }

            if (cardEffectList.TryGetValue(codename, out ReturnBaseCardEffect cardEffect))
            {
                BaseCardEffect newEffect = cardEffect();
                card.CardEffect = newEffect;
                newEffect.Card = card;
            }
            else
            {
                Debug.LogWarning("카드 생성 시" + codename + "코드 네임이 이펙트 데이터에 없음");
            }
        }
        if (parent)
        {
            result.transform.SetParent(parent);
        }
        //result.SetActive(false);
        return result;
    }

    public GameObject CopyFrom(GameObject original, Transform parent = null)
    {
        GameObject result = Object.Instantiate(original);
        //Card card = result.GetComponent<Card>();
        //card.InfoIdx = cardIdx;
        result.name = original.name;
        Card card = result.GetComponent<Card>();
        if (card)
        {
            card.CardEffect = original.GetComponent<Card>().CardEffect;
            card.CardEffect.Card = card;
        }
        if (parent)
        {
            result.transform.SetParent(parent);
        }
        //result.SetActive(false);
        return result;
    }
    public CardData GetCardData(int i)
    {
        return cardList.GetData(i);
    }

    public int GetCardDataCount()
    {
        return cardList.GetDataCount();
    }
}
