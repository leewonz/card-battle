using Con;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    PlayerCards playerCards;
    EnemyCardController enemyCardController;
    TeamController playerController;
    TeamController enemyController;

    public NumberTextMeshUI battleNumberText;
    public NumberTextMeshUI turnNumberText;

    public int battleNumber = 0;
    public int turnNumber = 0;

    public int BattleNumber
    {
        get { return battleNumber; }
        set
        {
            battleNumber = value;
            battleNumberText.FirstVal = battleNumber;
        }
    }

    public int TurnNumber
    {
        get { return turnNumber; }
        set
        {
            turnNumber = value;
            turnNumberText.FirstVal = turnNumber;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();

        //Field.Instance.GetSlot(Team.Opponent, 0).SummonCharacter(CardBuilder.Instance.CreateCard("mercExplorer"));
        //Field.Instance.GetSlot(Team.Opponent, 1).SummonCharacter(CardBuilder.Instance.CreateCard("mercExplorer"));
    }

    public void InitGame()
    {
        playerCards = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerCards>();
        playerController = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<TeamController>();
        enemyController = GameObject.FindGameObjectWithTag(Tags.enemy).GetComponent<TeamController>();
        enemyCardController = GameObject.FindGameObjectWithTag(Tags.enemy).GetComponent<EnemyCardController>();

        EventManager.Instance.DefeatEvent += OnTeamDefeat;

        playerController.InitGame();
        enemyController.InitGame();
        playerCards.InitGame();
    }

    public void InitBattle()
    {
        turnNumber = 0;

        playerController.InitBattle();
        enemyController.InitBattle();
        playerCards.InitBattle();
        enemyCardController.InitBattle();

        GetComponent<TurnStateMachine>().StartState(TurnStateMachine.TurnPhase.PlayerTurn);
    }

    public void WinBattle()
    {
        CommandManager.Instance.Clear();
        TurnStateMachine.Instance.ChangeState(TurnStateMachine.TurnPhase.BattleEnd);
        CommandManager.Instance.AddLast(new Command(WinBattleAction).SetName("WinBattleAction"));
    }

    public void LoseBattle()
    {
        CommandManager.Instance.Clear();
        TurnStateMachine.Instance.ChangeState(TurnStateMachine.TurnPhase.BattleEnd);
        CommandManager.Instance.AddLast(new Command(LoseBattleAction).SetName("LoseBattleAction"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerCards.Draw(1);
        }
    }


    private void OnTeamDefeat(object sender, DefeatEventArgs e)
    {
        switch(e.team)
        {
            case Team.Player:
                Debug.Log("당신 패배 >_<");
                LoseBattle();
                break;
            case Team.Opponent:
                Debug.Log("당신 승리 >_<");
                WinBattle();
                break;
        }
    }

    public float WinBattleAction()
    {
        GameManager.Instance.WinBattle();
        return 0.0f;
    }

    public float LoseBattleAction()
    {
        GameManager.Instance.LoseBattle();
        return 0.0f;
    }
}
