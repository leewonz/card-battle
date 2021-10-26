using System.Collections.Generic;
using UnityEngine;

public class Field : MonoSingleton<Field>
{
    public List<CharacterSlot> playerSlots;
    public List<CharacterSlot> enemySlots;
    // Start is called before the first frame update

    private void Awake()
    {
        for (int i = 0; i < playerSlots.Count; i++)
        {
            playerSlots[i].field = this;
        }

        for (int i = 0; i < enemySlots.Count; i++)
        {
            enemySlots[i].field = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCharacter(Team team, int slotNum, GameObject cardObj)
    {
        switch (team)
        {
            case Team.Player:
                playerSlots[slotNum].cardObject = cardObj;
                break;
            case Team.Opponent:
                enemySlots[slotNum].cardObject = cardObj;
                break;
        }
    }

    public GameObject GetCharacter(Team team, int slotNum)
    {

        switch (team)
        {
            case Team.Player:
                return playerSlots[slotNum].cardObject;
            case Team.Opponent:
                return playerSlots[slotNum].cardObject;
            default:
                return null;
        }
    }

    public void PlaySlotParticleAvailable(CardData data)
    {
        for (int i = 0; i < playerSlots.Count; i++)
        {
            if(playerSlots[i].IsCardTarget(data))
            {
                switch(data.cardType)
                {
                    case CardData.CardType.Merc:
                        playerSlots[i].SetParticle(CharacterSlot.EffectType.SummonAvailable, true);
                        break;
                    case CardData.CardType.Skill:
                        playerSlots[i].SetParticle(CharacterSlot.EffectType.SkillAvailable, true);
                        break;
                }
            }
        }

        for (int i = 0; i < enemySlots.Count; i++)
        {
            if (enemySlots[i].IsCardTarget(data))
            {
                switch (data.cardType)
                {
                    case CardData.CardType.Merc:
                        enemySlots[i].SetParticle(CharacterSlot.EffectType.SummonAvailable, true);
                        break;
                    case CardData.CardType.Skill:
                        enemySlots[i].SetParticle(CharacterSlot.EffectType.SkillAvailable, true);
                        break;
                }
            }
        }
    }

    public void PlaySlotParticleTargeted(CardData data, CharacterSlot slot)
    {
        if (data.cardEffectTarget == Team.Player || data.cardEffectTarget == Team.All)
        {
            for (int i = 0; i < playerSlots.Count; i++)
            {
                if (
                        (playerSlots[i].IsCardTarget(data) &&
                        data.cardEffectType == CardData.CardEffectType.One &&
                        slot == playerSlots[i]) ||
                        (playerSlots[i].IsCardTarget(data) &&
                        data.cardEffectType == CardData.CardEffectType.All &&
                        playerSlots[i].cardObject)
                    )
                {
                    switch (data.cardType)
                    {
                        case CardData.CardType.Merc:
                            playerSlots[i].SetParticle(CharacterSlot.EffectType.SummonTarget, true);
                            break;
                        case CardData.CardType.Skill:
                            playerSlots[i].SetParticle(CharacterSlot.EffectType.SkillTarget, true);
                            break;
                    }
                }
                else
                {
                    playerSlots[i].SetParticle(CharacterSlot.EffectType.SummonTarget, false);
                    playerSlots[i].SetParticle(CharacterSlot.EffectType.SkillTarget, false);
                }
            }
        }

        if (data.cardEffectTarget == Team.Opponent || data.cardEffectTarget == Team.All)
        {
            for (int i = 0; i < enemySlots.Count; i++)
            {
                if (
                        (enemySlots[i].IsCardTarget(data) &&
                        data.cardEffectType == CardData.CardEffectType.One &&
                        slot == enemySlots[i]) ||
                        (enemySlots[i].IsCardTarget(data) &&
                        data.cardEffectType == CardData.CardEffectType.All &&
                        enemySlots[i].cardObject)
                    )
                {
                    switch (data.cardType)
                    {
                        case CardData.CardType.Merc:
                            enemySlots[i].SetParticle(CharacterSlot.EffectType.SummonTarget, true);
                            break;
                        case CardData.CardType.Skill:
                            enemySlots[i].SetParticle(CharacterSlot.EffectType.SkillTarget, true);
                            break;
                    }
                }
                else
                {
                    enemySlots[i].SetParticle(CharacterSlot.EffectType.SummonTarget, false);
                    enemySlots[i].SetParticle(CharacterSlot.EffectType.SkillTarget, false);
                }
            }
        }
    }

    public void StopSlotParticle(CardData data)
    {
        for (int i = 0; i < playerSlots.Count; i++)
        {
            switch (data.cardType)
            {
                case CardData.CardType.Merc:
                    playerSlots[i].SetParticle(CharacterSlot.EffectType.SummonAvailable, false);
                    break;
                case CardData.CardType.Skill:
                    playerSlots[i].SetParticle(CharacterSlot.EffectType.SkillAvailable, false);
                    break;
            }

        }

        for (int i = 0; i < enemySlots.Count; i++)
        {
            switch (data.cardType)
            {
                case CardData.CardType.Merc:
                    enemySlots[i].SetParticle(CharacterSlot.EffectType.SummonAvailable, false);
                    break;
                case CardData.CardType.Skill:
                    enemySlots[i].SetParticle(CharacterSlot.EffectType.SkillAvailable, false);
                    break;
            }
        }
    }

    public void StopSlotParticleTargeted()
    {
        for (int i = 0; i < playerSlots.Count; i++)
        {
            playerSlots[i].SetParticle(CharacterSlot.EffectType.SummonTarget, false);
            playerSlots[i].SetParticle(CharacterSlot.EffectType.SkillTarget, false);
        }

        for (int i = 0; i < enemySlots.Count; i++)
        {
            enemySlots[i].SetParticle(CharacterSlot.EffectType.SummonTarget, false);
            enemySlots[i].SetParticle(CharacterSlot.EffectType.SkillTarget, false);
        }
    }

    public void TeamAttack(Team team)
    {
        switch (team)
        {
            case Team.Player:
                for (int i = playerSlots.Count - 1; i >= 0; i--)
                {
                    if(playerSlots[i].cardObject)
                    {
                        Character character = playerSlots[i].cardObject.GetComponent<Character>();
                        character.DoAttackSequence();
                    }
                }
                break;
            case Team.Opponent:
                for (int i = enemySlots.Count - 1; i >= 0; i--)
                {
                    if (enemySlots[i].cardObject)
                    {
                        Character character = enemySlots[i].cardObject.GetComponent<Character>();
                        character.DoAttackSequence();
                    }
                }
                break;
        }
    }

    public CharacterSlot GetSlot(Team team, int num)
    {
        switch (team)
        {
            case Team.Player:
                return playerSlots[num];
            case Team.Opponent:
                return enemySlots[num];
            default:
                return null;
        }
    }

    public List<CharacterSlot> GetList(Team team)
    {
        switch (team)
        {
            case Team.Player:
                return playerSlots;
            case Team.Opponent:
                return enemySlots;
            case Team.All:
                List<CharacterSlot> slots = new List<CharacterSlot>(playerSlots);
                slots.AddRange(enemySlots);
                return slots;
            default:
                return null;
        }
    }

    public int GetCharacterCount(Team team)
    {
        int result = 0;
        switch (team)
        {
            case Team.Player:
                for(int i = 0; i< playerSlots.Count; i++)
                {
                    if(playerSlots[i].cardObject) { result++; }
                }
                break;
            case Team.Opponent:
                for (int i = 0; i < enemySlots.Count; i++)
                {
                    if (enemySlots[i].cardObject) { result++; }
                }
                break;
            case Team.All:
                for (int i = 0; i < playerSlots.Count; i++)
                {
                    if (playerSlots[i].cardObject) { result++; }
                    if (enemySlots[i].cardObject) { result++; }
                }
                break;
            default:
                return -1;
        }
        return result;
    }

    public void UnsummonAll()
    {
        for (int i = 0; i < playerSlots.Count; i++)
        {
            playerSlots[i].UnsummonCharacter(CharacterSlot.UnsummonType.BySystem);
        }

        for (int i = 0; i < enemySlots.Count; i++)
        {
            enemySlots[i].UnsummonCharacter(CharacterSlot.UnsummonType.BySystem);
        }
    }
}
