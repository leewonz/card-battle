using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualWieldCardEffect : BaseCardEffect
{
    public override void Play(CharacterSlot slot) 
    { 
        if(slot && slot.cardObject)
        {
            slot.cardObject.GetComponent<Character>().IncreaseAttackCount(1);
        }
    }
}
