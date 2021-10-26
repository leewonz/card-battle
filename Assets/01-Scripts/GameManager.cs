using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public enum GameState
    {
        Battle,
        PickCard
    }
    public GameState gameState = GameState.Battle;
    public List<CardList> EnemyStages;

    BattleManager battleManager;
    EnemyCardController enemyCardController;
    PlayerCards playerCards;
    public PopupText popupText;

    // Start is called before the first frame update

    private void Awake()
    {
        battleManager = GameObject.FindGameObjectWithTag(Con.Tags.gameController).GetComponent<BattleManager>();
        enemyCardController = GameObject.FindGameObjectWithTag(Con.Tags.enemy).GetComponent<EnemyCardController>();
        playerCards = GameObject.FindGameObjectWithTag(Con.Tags.player).GetComponent<PlayerCards>();
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        battleManager.InitGame();

        NextBattle();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            EventManager.Instance.Invoke(EventManager.GameEventType.DefeatEvent, new DefeatEventArgs(Team.Opponent));
        }
    }

    public void NextBattle()
    {
        popupText.ShowText(PopupText.TextType.StartBattle);
        battleManager.BattleNumber++;
        enemyCardController.summonOnStart = EnemyStages[battleManager.battleNumber - 1];
        TurnStateMachine.Instance.ChangeState(TurnStateMachine.TurnPhase.BattleStart);
    }

    public void WinBattle()
    {
        popupText.ShowText(PopupText.TextType.WinBattle);
        CommandManager.Instance.AddLast(CommandManager.GetDelayCommand(1.6f));
        CommandManager.Instance.AddLast(new Command(AfterWinAction));

    }

    public void LoseBattle()    
    {
        popupText.ShowText(PopupText.TextType.LoseBattle);
        CommandManager.Instance.AddLast(CommandManager.GetDelayCommand(1.6f));
        CommandManager.Instance.AddLast(new Command(AfterLoseAction));
    }

    public float AfterWinAction()
    {
        Field.Instance.UnsummonAll();
        playerCards.hand.Clear();
        if(battleManager.BattleNumber >= EnemyStages.Count)
        {
            SceneManager.LoadScene("TempGameWinScene");
        }
        else
        {
            NextBattle();
        }

        return 0.0f;
    }

    public float AfterLoseAction()
    {
        Field.Instance.UnsummonAll();
        playerCards.hand.Clear();
        //NextBattle();
        SceneManager.LoadScene("TempGameLoseScene");

        return 0.0f;
    }
}
