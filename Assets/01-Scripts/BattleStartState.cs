using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStartState : IState
{
    public void Enter(int lastStateIndex)
    {
        CameraTransformController cameraController = 
            GameObject.FindGameObjectWithTag(Con.Tags.cameraParent).GetComponent<CameraTransformController>();

        cameraController.SetTransform(CameraTransformHolder.CameraTransformType.BattleStart, 0, true);
        cameraController.rate = CameraTransformController.slowRate;
        cameraController.SetTransform(CameraTransformHolder.CameraTransformType.Normal, 0);

        Debug.Log("Battle Start.");

        CommandManager.Instance.AddLast(CommandManager.GetDelayCommand(1.0f));
        CommandManager.Instance.AddLast(new Command(BattleStartAction));
    }

    public void Exit(int lastStateIndex)
    {

    }

    public int GetStateIndex()
    {
        return (int)TurnStateMachine.TurnPhase.BattleStart;
    }

    void IState.Update()
    {
    }

    public float BattleStartAction()
    {
        //TurnStateMachine.Instance.ChangeState(TurnStateMachine.TurnPhase.PlayerTurn);
        CameraTransformController cameraController =
            GameObject.FindGameObjectWithTag(Con.Tags.cameraParent).GetComponent<CameraTransformController>();
        cameraController.rate = CameraTransformController.normalRate;

        BattleManager battleManager =
            GameObject.FindGameObjectWithTag(Con.Tags.gameController).GetComponent<BattleManager>();
            battleManager.InitBattle();

        return 0.0f;
    }
}
