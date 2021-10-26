using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : Singleton<EventManager>
{
    public enum GameEventType
    {
        DrawCard,
        PlayCard,
        DiscardCard,
        Summon,
        TakeDamageEvent,
        HealedEvent,
        DieEvent,
        ReplacedEvent,
        StartTurnEvent,
        EndTurnEvent,
        DefeatEvent,
    }

    public enum EffectSource
    {
        Merc,
        Skill,
        Direct
    }

    //public delegate void DrawCardDelegate(DrawCardEventArgs e);
    public event EventHandler<DrawCardEventArgs> DrawCardEvent;
    public event EventHandler<PlayCardEventArgs> PlayCardEvent;
    public event EventHandler<DiscardCardEventArgs> DiscardCardEvent;
    public event EventHandler<SummonEventArgs> SummonEvent;
    public event EventHandler<TakeDamageEventArgs> TakeDamageEvent;
    public event EventHandler<HealedEventArgs> HealedEvent;
    public event EventHandler<DieEventArgs> DieEvent;
    public event EventHandler<ReplacedEventArgs> ReplacedEvent;
    public event EventHandler<StartTurnEventArgs> StartTurnEvent;
    public event EventHandler<EndTurnEventArgs> EndTurnEvent;
    public event EventHandler<DefeatEventArgs> DefeatEvent;

    protected override void Init()
    {

    }

    public void Invoke(GameEventType type, EventArgs e)
    {
        switch(type)
        {
            case GameEventType.DrawCard:
                DrawCardEvent?.Invoke(this, (DrawCardEventArgs)e);
                break;
            case GameEventType.PlayCard:
                PlayCardEvent?.Invoke(this, (PlayCardEventArgs)e);
                break;
            case GameEventType.DiscardCard:
                DiscardCardEvent?.Invoke(this, (DiscardCardEventArgs)e);
                break;
            case GameEventType.Summon:
                SummonEvent?.Invoke(this, (SummonEventArgs)e);
                break;
            case GameEventType.StartTurnEvent:
                StartTurnEvent?.Invoke(this, (StartTurnEventArgs)e);
                break;
            case GameEventType.EndTurnEvent:
                EndTurnEvent?.Invoke(this, (EndTurnEventArgs)e);
                break;
            case GameEventType.DefeatEvent:
                DefeatEvent?.Invoke(this, (DefeatEventArgs)e);
                break;
            default:
                Debug.LogError(Enum.GetName(typeof(GameEventType), type) + "은 사용하지 않는 이벤트임.");
                break;
        }
    }
}

public class DrawCardEventArgs : EventArgs
{
    public GameObject DrawnCard { get; set; }

    public DrawCardEventArgs(GameObject drawnCard)
    {
        DrawnCard = drawnCard;
    }
}
public class PlayCardEventArgs : EventArgs
{
    public GameObject PlayedCard { get; set; }

    public PlayCardEventArgs(GameObject playedCard)
    {
        PlayedCard = playedCard;
    }
}
public class DiscardCardEventArgs : EventArgs
{
    public GameObject DiscardedCard { get; set; }

    public DiscardCardEventArgs(GameObject discardedCard)
    {
        DiscardedCard = discardedCard;
    }
}
public class SummonEventArgs : EventArgs
{
    public GameObject SummonedCard { get; set; }
    public CharacterSlot Slot { get; set; }

    public SummonEventArgs(GameObject summonedCard, CharacterSlot slot)
    {
        SummonedCard = summonedCard;
        Slot = slot;
    }
}

public class TakeDamageEventArgs : EventArgs
{
    public CardData.CardType DamageSource { get; set; }
    public GameObject Attacker { get; set; }
    public GameObject Victim { get; set; }
    public int Damage { get; set; }

    public TakeDamageEventArgs(CardData.CardType damageSource, GameObject attacker, GameObject victim, int damage)
    {
        DamageSource = damageSource;
        Attacker = attacker;
        Victim = victim;
        Damage = damage;
    }
}

public class HealedEventArgs : EventArgs
{
    public CardData.CardType HealSource { get; set; }
    public GameObject Healer { get; set; }
    public GameObject Healee { get; set; }
    public int HealAmount { get; set; }

    public HealedEventArgs(CardData.CardType healSource, GameObject healer, GameObject healee, int healAmount)
    {
        HealSource = healSource;
        Healer = healer;
        Healee = healee;
        HealAmount = healAmount;
    }
}

public class DieEventArgs : EventArgs
{
    public CardData.CardType KillSource { get; set; }
    public GameObject Attacker { get; set; }
    public GameObject Victim { get; set; }

    public DieEventArgs(CardData.CardType killSource, GameObject attacker, GameObject victim)
    {
        KillSource = killSource;
        Attacker = attacker;
        Victim = victim;
    }
}

public class ReplacedEventArgs : EventArgs
{
    public CardData.CardType KillSource { get; set; }
    public GameObject OldCharacter { get; set; }
    public GameObject NewCharacter { get; set; }

    public ReplacedEventArgs(GameObject oldCharacter, GameObject newCharacter)
    {
        OldCharacter = oldCharacter;
        NewCharacter = newCharacter;
    }
}

public class StartTurnEventArgs : EventArgs
{
    public Team team { get; set; }
    public int turnNum{ get; set; }

    public StartTurnEventArgs(Team team, int turnNum)
    {
        this.team = team;
        this.turnNum = turnNum;
    }
}

public class EndTurnEventArgs : EventArgs
{
    public Team team { get; set; }
    public int turnNum { get; set; }

    public EndTurnEventArgs(Team team, int turnNum)
    {
        this.team = team;
        this.turnNum = turnNum;
    }
}

public class DefeatEventArgs : EventArgs
{
    public Team team { get; set; }

    public DefeatEventArgs(Team team)
    {
        this.team = team;
    }
}


//public class Test
//{

//    static void c_ThresholdReached<T>(object sender, T args) where T : DrawCardEventArgs
//    {
//        //(args.DrawnCard.name);
//    }

//    public void Sub()
//    {
//        EventManager.Instance.DrawCardEvent += c_ThresholdReached;
//    }
//}