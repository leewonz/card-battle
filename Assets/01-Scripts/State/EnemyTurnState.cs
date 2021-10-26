using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : IState
{
    public void Enter(int lastStateIndex)
    {
        Debug.Log("Enemy's Turn Started.");

        if (Field.Instance.GetCharacterCount(Team.Opponent) == 0)
        {
            EventManager.Instance.Invoke(EventManager.GameEventType.DefeatEvent, new DefeatEventArgs(Team.Opponent));
        }

        BattleManager battleManager = GameObject.FindGameObjectWithTag(Con.Tags.gameController).
            GetComponent<BattleManager>();

        EventManager.Instance.Invoke(
            EventManager.GameEventType.StartTurnEvent, new StartTurnEventArgs(Team.Opponent, battleManager.TurnNumber));
    }

    public void Exit(int lastStateIndex)
    {
        
    }

    public int GetStateIndex()
    {
        return (int)TurnStateMachine.TurnPhase.EnemyTurn;
    }

    public void Update()
    {
        if (CommandManager.Instance.state == CommandManager.State.Empty)
        {
            TurnStateMachine ts =
                GameObject.FindGameObjectWithTag(Con.Tags.gameController).GetComponent<TurnStateMachine>();
            ts.ChangeState(TurnStateMachine.TurnPhase.EnemyAttack);
        }
    }
}
