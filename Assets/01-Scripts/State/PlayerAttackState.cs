using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IState
{
    public void Enter(int lastStateIndex)
    {
        Debug.Log("Player's Attack Started.");

        CommandManager.Instance.AddLast(CommandManager.GetDelayCommand(0.2f));
        Field.Instance.TeamAttack(Team.Player);
    }

    public void Exit(int lastStateIndex)
    {
        
    }

    public int GetStateIndex()
    {
        return (int)TurnStateMachine.TurnPhase.PlayerTurn;
    }

    public void Update()
    {
        if(CommandManager.Instance.state == CommandManager.State.Empty)
        {
            TurnStateMachine ts =
                GameObject.FindGameObjectWithTag(Con.Tags.gameController).GetComponent<TurnStateMachine>();
            ts.ChangeState(TurnStateMachine.TurnPhase.EnemyTurn);
        }
    }
}
