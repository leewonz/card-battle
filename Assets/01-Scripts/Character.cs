using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum RecivedEffectSource
    {
        Merc,
        Skill
    }

    private readonly string characterObjNamePrefix = "Char - ";

    public Card card;
    public int healthPoint;
    public int healthPointMax;
    public int attackPoint;
    public int attackPointMax;
    int attackCount;
    public int armorPoint;

    CardData cardData;
    GameObject model;
    public CardData CardData
    {
        get
        {
            return cardData;
        }
        set
        {
            cardData = value;
            //CardArtRenderer.sprite = cardData.sprite;
            gameObject.name = characterObjNamePrefix + cardData.name;
            healthPoint = healthPointMax = cardData.healthPoint;
            attackPoint = cardData.attackPoint;
            attackCount = cardData.attackCount;

            RefreshCharacterUI();
        }
    }

    public GameObject Model
    {
        get
        {
            return model;
        }
        set
        {
            model = value;
            if(cardData)
            {
                model.GetComponent<CharacterModelSelector>().SelectModel(cardData.characterCodename);
            }
            else
            {
                Debug.LogWarning("캐릭터 모델이 없음!");
            }
        }
    }

    public int AttackCount
    {
        get { return attackCount; }
        set { attackCount = value; RefreshCharacterUI(); }
    }

    public void Init(Card card, CardData cardData)
    {
        this.card = card;
        CardData = cardData;
    }

    public void Release()
    {

    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(CharacterSlot slot, int count)
    {
        var attackCommand = 
            new Command<Character, CharacterSlot, int, int>(PrepareAttackAction, this, slot, attackPoint, count);

        CommandManager.Instance.AddLast(attackCommand);
    }

    public void DoAttackSequence()
    {
        CharacterSlot oppositeSlot = card.Slot.field.GetSlot(DataManager.OppositeTeam(card.Slot.team), card.Slot.slotNum);
        Attack(oppositeSlot, attackCount);
    }

    public void TakeDamage(RecivedEffectSource source, Character attacker, int damage)
    {
        Animator anim = Model.GetComponentInChildren<Animator>();
        
        healthPoint -= damage;
        GameObject damageText = 
            ObjectPool.Instance.Spawn("DamageText", Model.transform.position, Model.transform.rotation);
        GameObject.FindGameObjectWithTag(Con.Tags.mainCamera).GetComponent<CameraShake>().
            Begin(damage);
        card.Slot.SetParticle(CharacterSlot.EffectType.DamageBurst, true);
        damageText.GetComponentInChildren<NumberTextMeshUI>().FirstVal = damage;
        RefreshCharacterUI();

        if (healthPoint <= 0)
        {
            anim.SetTrigger("Death");
            card.Slot.UnsummonCharacter(CharacterSlot.UnsummonType.Die);
        }
        else
        {
            card.CardEffect.TakeDamage(source, attacker, damage);
            anim.SetTrigger("Damaged");
        }

    }

    public void RecieveHeal(RecivedEffectSource source, Character healer, int healAmount)
    {
        CommandManager.Instance.AddLast(
            new Command<RecivedEffectSource, Character, Character, int>(HealAction, source, healer, this, healAmount)
            .SetName("Heal Slot " + card.Slot.slotNum + " for " + healAmount)
            );
        //EventManager.Instance.Invoke(EventManager.EventTypes.)
    }

    public void IncreaseAttackCount(int increaseAmount)
    {
        CommandManager.Instance.AddFirst(
            new Command<Character, int>(IncreaseAttackCountAction, this, increaseAmount));
    }

    public void UnsummonBySystem()
    {
        //Slot.UnsummonCharacter(CharacterSlot.UnsummonType.Replaced);
        card.CardEffect.Unsummon();

        Destroy(Model);
    }

    public void Replaced()
    {
        //Slot.UnsummonCharacter(CharacterSlot.UnsummonType.Replaced);
        card.CardEffect.Unsummon();

        var destroyCommand = new Command<GameObject>(DestroyAction, Model);

        CommandManager.Instance.AddFirst(destroyCommand);
    }

    public void Die()
    {
        //Slot.UnsummonCharacter(CharacterSlot.UnsummonType.Die);
        card.CardEffect.Unsummon();

        if (Field.Instance.GetCharacterCount(Team.Opponent) == 0)
        {
            EventManager.Instance.Invoke(EventManager.GameEventType.DefeatEvent, new DefeatEventArgs(Team.Opponent));
        }

        var destroyCommand = new Command<GameObject>(DestroyAction, Model);

        CommandManager.Instance.AddFirst(destroyCommand);
        CommandManager.Instance.AddFirst(CommandManager.GetDelayCommand(0.6f));
    }

    public float PrepareAttackAction(Character attacker, CharacterSlot victim, int damage, int count)
    {

        CameraTransformController transformController;

        if (victim.cardObject)
        {
            if (GameObject.FindGameObjectWithTag(Con.Tags.cameraParent).
                TryGetComponent<CameraTransformController>(out transformController))
            {
                transformController.SetTransform(victim.team, victim.slotNum);
            }
            CommandManager.Instance.AddFirst(CommandManager.GetDelayCommand(0.2f));
        }
        else
        {
            if (GameObject.FindGameObjectWithTag(Con.Tags.cameraParent).
                TryGetComponent<CameraTransformController>(out transformController))
            {
                transformController.SetTransform(CameraTransformHolder.CameraTransformType.Normal, 0);
            }
            CommandManager.Instance.AddFirst(CommandManager.GetDelayCommand(0.2f));
        }

        for (int i = 0; i < count; i++)
        {
            CommandManager.Instance.AddFirst(
                new Command<Character, CharacterSlot, int>(AttackAction, attacker, victim, damage));
        }

        return 0.4f;
    }

    public float AttackAction(Character attacker, CharacterSlot victim, int damage)
    {
        Animator anim = Model.GetComponentInChildren<Animator>();
        anim.SetTrigger("Attack");
        if (victim.cardObject)
        {
            victim.cardObject.GetComponent<Character>().TakeDamage(
                RecivedEffectSource.Merc, attacker, damage);
            return 0.4f;
        }
        else
        {
            TeamController playerController;
                        
            switch (victim.team)
            {
                case Team.Player:
                    playerController = GameObject.FindGameObjectWithTag(Con.Tags.player).GetComponent<TeamController>();
                    break;
                case Team.Opponent:
                    playerController = GameObject.FindGameObjectWithTag(Con.Tags.enemy).GetComponent<TeamController>();
                    break;
                default:
                    return 0.0f;
            }
            playerController.TakeDamage(damage);
            return 0.4f;
        }
    }

    public float DestroyAction(GameObject gameObject)
    {
        Destroy(gameObject);
        return 0.0f;
    }

    public float HealAction(RecivedEffectSource source, Character healer, Character healee, int healAmount)
    {
        if (healee && (healee.healthPoint < healee.healthPointMax))
        {
            //character.RecieveHeal(Character.RecivedEffectSource.Skill, null, healAmount, false);

            int healthPreHeal = healee.healthPoint;
            int healedAmount;
            healee.healthPoint = Mathf.Min(healthPoint + healAmount, healee.healthPointMax);
            healedAmount = healthPoint - healthPreHeal;
            GameObject healText =
                ObjectPool.Instance.Spawn("HealText", Model.transform.position, Model.transform.rotation);
            card.Slot.SetParticle(CharacterSlot.EffectType.HealBurst, true);
            healText.GetComponentInChildren<NumberTextMeshUI>().FirstVal = healedAmount;

            card.CardEffect.RecieveHeal(source, healer, healedAmount);

            RefreshCharacterUI();

            return 0.2f;
        }
        return 0.0f;
    }

    public float IncreaseAttackCountAction(Character reciever, int increaseAmount)
    {
        if (reciever)
        {
            reciever.AttackCount += increaseAmount;
            card.Slot.SetParticle(CharacterSlot.EffectType.BuffBurst, true);
            RefreshCharacterUI();

            return 0.2f;
        }
        return 0.0f;
    }

    public float IncreaseAttackPointAction(Character reciever, int increaseAmount)
    {
        if (reciever)
        {
            reciever.attackPoint += increaseAmount;
            card.Slot.SetParticle(CharacterSlot.EffectType.BuffBurst, true);
            RefreshCharacterUI();

            return 0.2f;
        }
        return 0.0f;
    }

    public void RefreshCharacterUI()
    {
        if (card.Slot)
        {
            card.Slot.characterUI.healthPointText.FirstVal = healthPoint;
            card.Slot.characterUI.healthPointText.SecondVal = healthPointMax;
            card.Slot.characterUI.attackPointText.FirstVal = attackPoint;
            card.Slot.characterUI.attackPointText.SecondVal = attackCount;
        }
        else
        {
            Debug.LogError("Slot이 없음.");
        }    
    }
}
