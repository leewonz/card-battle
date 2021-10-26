using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class BaseCardEffect
{
    public Card card;
    public Card Card { get { return card; } set { card = value; } }
    virtual public void Play(CharacterSlot slot) { }
    virtual public void Summon(CharacterSlot slot) { }
    virtual public void Draw() { }
    virtual public void Attack(CharacterSlot victim, int damage) { }
    virtual public void RecieveHeal(Character.RecivedEffectSource source, Character healer, int healAmount) { }
    virtual public void TakeDamage(Character.RecivedEffectSource source, Character attacker, int damage) { }
    virtual public void Unsummon() { }

    public static T ReturnObject<T>() where T : BaseCardEffect, new() { return new T(); }
}
