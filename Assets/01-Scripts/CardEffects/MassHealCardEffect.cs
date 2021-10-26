using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Con;

[Serializable]
public class MassHealCardEffect : BaseCardEffect
{
    public override void Play(CharacterSlot slot)
    {
        Field field = 
            GameObject.FindGameObjectWithTag(Con.Tags.gameController).GetComponent<Field>();
        List<CharacterSlot> slots = field.GetList(Team.Player);
        int healAmount = 2;
        for(int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].cardObject) { continue; }
            if (slots[i].cardObject.TryGetComponent<Character>(out Character character))
            {
                character.RecieveHeal(Character.RecivedEffectSource.Skill, null, healAmount);
            }
        }
        CommandManager.Instance.PrintCurrentCommandNames();
    }
}
