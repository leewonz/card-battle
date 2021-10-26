using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEndState : IState
{
    public void Enter(int lastStateIndex)
    {
        Debug.Log("Battle End.");
    }

    public void Exit(int lastStateIndex)
    {

    }

    public int GetStateIndex()
    {
        return (int)TurnStateMachine.TurnPhase.BattleEnd;
    }

    void IState.Update()
    {
    }
}
