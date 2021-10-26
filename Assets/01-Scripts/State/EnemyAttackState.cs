using Con;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState
{
    public void Enter(int lastStateIndex)
    {
        Debug.Log("Enemy's Attack Started.");

        Field.Instance.TeamAttack(Team.Opponent);
    }

    public void Exit(int lastStateIndex)
    {
        
    }

    public int GetStateIndex()
    {
        return (int)TurnStateMachine.TurnPhase.EnemyAttack;
    }

    public void Update()
    {
        if (CommandManager.Instance.state == CommandManager.State.Empty)
        {
            TurnStateMachine ts =
                GameObject.FindGameObjectWithTag(Con.Tags.gameController).GetComponent<TurnStateMachine>();
            ts.ChangeState(TurnStateMachine.TurnPhase.PlayerTurn);
        }
    }
}
