using Con;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamController : MonoBehaviour
{
    public PlayerData playerData;

    public Team team;
    public NumberTextMeshUI healthNumberText;
    public NumberTextMeshUI manaNumberText;

    public int maxHealthPoint;
    public int maxHealthPointPermanant;
    public int drawCountPerTurn;
    public int drawCountPerTurnPermanant;
    public int manaGainPerTurn;
    public int manaGainPerTurnPermanant;
    public int maxMana;
    public int maxManaPermanant;

    int healthPoint;
    int mana;

    public int Mana
    {
        get
        {
            return mana;
        }
        set
        {
            mana = value;
            if (manaNumberText)
            {
                manaNumberText.FirstVal = mana;
            }
        }
    }

    public int HealthPoint
    {
        get
        {
            return healthPoint;
        }
        set
        {
            healthPoint = value;
            if (healthNumberText)
            {
                healthNumberText.FirstVal = healthPoint;
                healthNumberText.SecondVal = maxHealthPoint;
            }
        }
    }

    public void InitGame()
    {
        drawCountPerTurnPermanant = playerData.drawCountPerTurn;
        manaGainPerTurnPermanant = playerData.manaGainPerTurn;
        maxHealthPointPermanant = maxHealthPoint = playerData.maxHealth;
        maxManaPermanant = playerData.maxMana;
        if(team == Team.Player)
        {
            HealthPoint = maxHealthPoint = maxHealthPointPermanant;
        }
    }

    public void InitBattle()
    {
        drawCountPerTurn = drawCountPerTurnPermanant;
        Mana = manaGainPerTurn = manaGainPerTurnPermanant;
        maxMana = maxManaPermanant;
        if (team == Team.Opponent)
        {
            HealthPoint = maxHealthPoint = maxHealthPointPermanant;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTurn()
    {

    }

    public void EndTurn()
    {
        TurnStateMachine ts =
            GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<TurnStateMachine>();

        //Mana = Mathf.Max(Mana, manaGainPerTurn);
        Mana = manaGainPerTurn;

        switch (team)
        {
            case Team.Player:
                if(ts.CurrPhase == TurnStateMachine.TurnPhase.PlayerTurn)
                {
                    ts.ChangeState(TurnStateMachine.TurnPhase.PlayerAttack);
                }
                break;
            case Team.Opponent:
                if (ts.CurrPhase == TurnStateMachine.TurnPhase.EnemyTurn)
                {
                    ts.ChangeState(TurnStateMachine.TurnPhase.EnemyAttack);
                }
                break;
        }
    }


    public void TakeDamage(int damage)
    {
        this.HealthPoint -= damage;
        if (healthNumberText)
        {
            //GameObject damageText = ObjectPool.Instance.Spawn(
            //    "DamageBaseText", healthNumberText.gameObject.transform.position, healthNumberText.gameObject.transform.rotation);
            GameObject damageText = ObjectPool.Instance.Spawn(
                "DamageBaseText", healthNumberText.transform);
            damageText.transform.position = healthNumberText.transform.position;
            damageText.transform.rotation = healthNumberText.transform.rotation;
            damageText.GetComponentInChildren<NumberTextMeshUI>().FirstVal = damage;

            GameObject.FindGameObjectWithTag(Con.Tags.mainCamera).GetComponent<CameraShake>().
                Begin(damage);
        }
        if(HealthPoint <= 0)
        {
            EventManager.Instance.Invoke(EventManager.GameEventType.DefeatEvent, new DefeatEventArgs(team));
        }
    }
}
