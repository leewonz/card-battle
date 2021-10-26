using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCardEffect : BaseCardEffect
{
    override public void Play(CharacterSlot slot) 
    {
        if(slot.cardObject.TryGetComponent<Character>(out var character))
        {
            character.DoAttackSequence();
            if (GameObject.FindGameObjectWithTag(Con.Tags.cameraParent).
                TryGetComponent<CameraTransformController>(out var transformController))
            {
                CommandManager.Instance.AddLast(
                    transformController.GetTransformCommand(CameraTransformHolder.CameraTransformType.Normal, 0));
            }
        }
    }
}
