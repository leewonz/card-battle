using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcWarriorCardEffect : BaseCardEffect
{
    public override void TakeDamage(Character.RecivedEffectSource source, Character attacker, int damage)
    {
        if (Card.TryGetComponent<Character>(out var character))
        {
            CommandManager.Instance.AddFirst(
                new Command<Character, int>(character.IncreaseAttackPointAction, character, 1));
            CommandManager.Instance.PrintCurrentCommandNames();
        }
    }
}
