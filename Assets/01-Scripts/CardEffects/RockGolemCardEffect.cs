using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemCardEffect : BaseCardEffect
{
    public override void RecieveHeal(Character.RecivedEffectSource source, Character healer, int healAmount)
    {
        if (Card.TryGetComponent<Character>(out var character))
        {
            CommandManager.Instance.AddFirst(
                new Command<Character, int>(character.IncreaseAttackPointAction, character, healAmount));
            CommandManager.Instance.PrintCurrentCommandNames();
        }
    }
}
