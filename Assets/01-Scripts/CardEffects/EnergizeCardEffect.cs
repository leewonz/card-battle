using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Con;

[Serializable]
public class EnergizeCardEffect : BaseCardEffect
{
    public int manaGain = 2;
    public override void Play(CharacterSlot slot)
    {
        GameObject go = GameObject.FindGameObjectWithTag(Tags.player);
        TeamController pc = go.GetComponent<TeamController>();
        if (pc)
        {
            pc.Mana += manaGain;
        }
    }
}
