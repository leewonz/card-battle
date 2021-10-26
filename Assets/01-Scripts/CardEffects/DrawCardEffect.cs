using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Con;

[Serializable]
public class DrawCardEffect : BaseCardEffect
{
    public override void Play(CharacterSlot slot)
    {
        Command<int> command = new Command<int>(DrawCommand, 2);
        CommandManager.Instance.AddLast(command);
    }

    public float DrawCommand(int i)
    {
        GameObject go = GameObject.FindGameObjectWithTag(Tags.player);
        PlayerCards pc = go.GetComponent<PlayerCards>();
        if (pc)
        {
            pc.Draw(i);
            return 1.0f;
        }
        return 0.0f;
    }
}
