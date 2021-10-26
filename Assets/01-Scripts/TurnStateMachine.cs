using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnStateMachine : MonoSingleton<TurnStateMachine>
{
    [Serializable]
    public enum TurnPhase
    {
        PlayerTurn,
        PlayerAttack,
        EnemyTurn,
        EnemyAttack,
        BattleStart,
        BattleEnd
    }

    [SerializeField]
    TurnPhase currPhase = TurnPhase.PlayerTurn;
    public TurnPhase CurrPhase { get { return currPhase; } }

    Dictionary<TurnPhase, IState> states = new Dictionary<TurnPhase, IState>();

    private void Awake()
    {
        if(states != null)
        {
            states.Add(TurnPhase.PlayerTurn, new PlayerTurnState());
            states.Add(TurnPhase.PlayerAttack, new PlayerAttackState());
            states.Add(TurnPhase.EnemyTurn, new EnemyTurnState());
            states.Add(TurnPhase.EnemyAttack, new EnemyAttackState());
            states.Add(TurnPhase.BattleStart, new BattleStartState());
            states.Add(TurnPhase.BattleEnd, new BattleEndState());
        }
        else
        {
            Debug.LogError("EEEEEEEEEEEEEEEe");
        }
    }

    private void Start()
    {
        //StartState(TurnPhase.PlayerTurn);
    }

    private void Update()
    {
        if (states.TryGetValue(currPhase, out IState currState))
        {
            currState.Update();
        }
        else
        {
            print(currPhase.ToString() + "is not TurnStateMachine");
        }
    }

    public void ChangeState(TurnPhase nextPhase)
    {
        TurnPhase prevPhase = currPhase;
        currPhase = nextPhase;

        if (prevPhase == nextPhase)
        {
            Debug.LogWarning(currPhase.ToString() + " -> " + nextPhase.ToString() + "같은 스테이트로 바꾸려 함");
        }

        if (!states.TryGetValue(nextPhase, out IState nextState))
        {
            Debug.LogWarning(currPhase.ToString() + " -> " + nextPhase.ToString() + "다음 스테이트가 없음");
        }

        if (!states.TryGetValue(prevPhase, out IState prevState))
        {
            Debug.Log(currPhase.ToString() + " -> " + nextPhase.ToString() + "기존 스테이트가 없음");
            nextState.Enter(-1);
        }

        prevState.Exit((int)nextPhase);
        nextState.Enter((int)currPhase);

    }

    public void StartState(TurnPhase phase)
    {
        if (states.TryGetValue(phase, out IState nextState))
        {
            currPhase = phase;
            nextState.Enter(-1);
        }
    }
}
