using Con;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : IState
{
    public void Enter(int lastStateIndex)
    {
        Debug.Log("Player's Turn Started.");

        GameObject go = GameObject.FindGameObjectWithTag(Tags.player);
        PlayerCards playerCards = go.GetComponent<PlayerCards>();
        TeamController playerController = go.GetComponent<TeamController>();
        BattleManager battleManager = GameObject.FindGameObjectWithTag(Con.Tags.gameController).
            GetComponent<BattleManager>();

        if (Field.Instance.GetCharacterCount(Team.Opponent) == 0)
        {
            EventManager.Instance.Invoke(EventManager.GameEventType.DefeatEvent, new DefeatEventArgs(Team.Opponent));
        }

        battleManager.TurnNumber++;
        //PlayerCards pc = go.GetComponent<PlayerCards>();

        if(GameObject.FindGameObjectWithTag(Con.Tags.mainCamera).
            TryGetComponent<FovController>(out var fovController))
        {
            fovController.targetFov = FovController.baseFov;
        }

        if (GameObject.FindGameObjectWithTag(Con.Tags.cameraParent).
            TryGetComponent<CameraTransformController>(out var transformController))
        {
            CommandManager.Instance.AddLast(
                transformController.GetTransformCommand(CameraTransformHolder.CameraTransformType.Normal, 0));
        }

        if(playerController)
        {
            playerController.StartTurn();
        }

        if (playerCards)
        {
            playerCards.StartTurn();
        }

        EventManager.Instance.Invoke(
            EventManager.GameEventType.StartTurnEvent, new StartTurnEventArgs(Team.Player, battleManager.TurnNumber));
    }

    public void Exit(int lastStateIndex)
    {
        GameObject go = GameObject.FindGameObjectWithTag(Tags.player);
        PlayerCards playerCards = go.GetComponent<PlayerCards>();
        TeamController playerController = go.GetComponent<TeamController>();
        BattleManager battleManager = GameObject.FindGameObjectWithTag(Con.Tags.gameController).
            GetComponent<BattleManager>();


        if (GameObject.FindGameObjectWithTag(Con.Tags.mainCamera).
            TryGetComponent<FovController>(out var fovController))
        {
            fovController.targetFov = FovController.focusFov;
        }

        playerCards.hand.IsPlayable = false;

        if (playerController)
        {
            playerController.EndTurn();
        }

        if (playerCards)
        {
            playerCards.EndTurn();
        }

        EventManager.Instance.Invoke(
            EventManager.GameEventType.EndTurnEvent, new StartTurnEventArgs(Team.Player, battleManager.TurnNumber));
    }

    public int GetStateIndex()
    {
        return (int)TurnStateMachine.TurnPhase.PlayerAttack;
    }

    public void Update()
    {
        
    }
}
