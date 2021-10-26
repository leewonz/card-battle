using Con;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IClickable
{
    public enum EffectType
    {
        SummonAvailable,
        SummonTarget,
        SkillAvailable,
        SkillTarget,
        BuffBurst,
        HealBurst,
        DamageBurst,
        FireBurst,
        SummonBurst
    }

    public enum UnsummonType
    {
        Die,
        Replaced,
        BySystem
    }

    public Team team;
    public int slotNum;
    public Field field;
    public GameObject cardObject;
    public MeshRenderer slotModel;
    public Material defaultMaterial;
    public Material hoverMaterial;
    public CharacterUI characterUI;
    public SerializableDictionary<EffectType, ParticleSystem> particleSystems;
    //public ParticleSystem particleSummonAvailable;
    //public ParticleSystem particleSummonTarget;
    //public ParticleSystem particleSpellAvaliable;
    //public ParticleSystem particleSpellTarget;


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
    //void AddCharacter(GameObject cardObj)
    //{
    //    this.cardObject = cardObj;
    //}


    //public void RemoveCharacter()
    //{

    //}

    public void OnPointerEnter(PointerEventData data)
    {
        slotModel.material = hoverMaterial;
        Debug.Log("Slot Enter " + data.position);
    }

    public void OnPointerExit(PointerEventData data)
    {
        slotModel.material = defaultMaterial;
        Debug.Log("Slot Exit " + data.position);
    }


    public void OnMouseUp()
    {

    }

    public void OnClickUp()
    {
        //print("OnClickUp");
        //InputManager.Instance.SlotUp(this);
    }

    public void OnClickDown()
    {

    }

    public void SummonCharacter(GameObject newCardObject)
    {
        if (newCardObject.TryGetComponent<Card>(out Card card))
        {
            if(cardObject)
            {
                UnsummonCharacter(UnsummonType.Replaced);
            }

            this.cardObject = newCardObject;
            newCardObject.transform.SetParent(transform);
            newCardObject.transform.localPosition = Vector3.zero;
            newCardObject.transform.localRotation = Quaternion.identity;

            characterUI.gameObject.SetActive(true);

            card.Summon(this);

            SetParticle(EffectType.SummonBurst, true);
            //field.AddCharacter(team, slotNum, cardObj);
        }
        else
        {
            Debug.LogError("Card 컴포넌트가 없는 카드를 슬롯에 등록");
        }
    }

    //public void UnsummonCharacter(GameObject cardObj)
    //{
    //    if (cardObj.TryGetComponent<Card>(out Card card))
    //    {
    //        this.cardObject = null;

    //        characterUI.gameObject.SetActive(false);

    //        card.Unsummon(UnsummonType.BySystem);
    //    }
    //    else
    //    {
    //        Debug.LogError("Card 컴포넌트가 없는 카드를 슬롯서 삭제");
    //    }
    //}

    public void UnsummonCharacter(UnsummonType type)
    {
        if(cardObject)
        {
            if (cardObject.TryGetComponent<Card>(out Card card))
            {
                card.Unsummon(type);

                this.cardObject = null;

                characterUI.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Card 컴포넌트가 없는 카드를 슬롯서 삭제");
            }
        }
    }

    public bool IsCardTarget(CardData cardData)
    {
        switch (cardData.cardType)
        {
            case CardData.CardType.Skill:
                switch (cardData.cardEffectType)
                {
                    case CardData.CardEffectType.Direct:
                        return true;

                    case CardData.CardEffectType.All:
                        if (team == cardData.cardEffectTarget ||
                            cardData.cardEffectTarget == Team.All)
                        {
                            return true;
                        }
                        break;

                    case CardData.CardEffectType.One:
                        if ((team == cardData.cardEffectTarget ||
                            cardData.cardEffectTarget == Team.All) &&
                            cardObject)
                        {
                            return true;
                        }
                        break;
                }
                break;
            case CardData.CardType.Merc:
                if (team == cardData.cardEffectTarget)
                {
                    return true;
                }
                break;
        }
        return false;
    }

    public void SetParticle(EffectType type, bool isActive)
    {
        ParticleSystem ps = particleSystems[type];
        if (ps)
        {
            switch (isActive)
            {
                case true:
                    ps.Play();
                    break;
                case false:
                    ps.Stop(true);
                    break;
            }
        }
    }

    public void StopAllParticle()
    {
        foreach (var keyValuePair in particleSystems)
        {
            keyValuePair.Value.Stop();
        }
    }

    private void OnEnable()
    {
        //Debug.Log("OnEnable");
    }
    private void OnDisable()
    {
        //Debug.Log("OnDisable");
    }
}
