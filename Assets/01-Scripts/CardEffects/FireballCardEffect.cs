using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCardEffect : BaseCardEffect
{
    public override void Play(CharacterSlot slot)
    {
        int damage = 2;
        slot.SetParticle(CharacterSlot.EffectType.FireBurst, true);
        slot.cardObject.GetComponent<Character>().TakeDamage(Character.RecivedEffectSource.Skill, null, damage);
        //if (Camera.main.TryGetComponent<CameraTransformController>(out var cameraTransformController))
        //{
        //    cameraTransformController.QueueTransform(CameraTransformHolder.CameraTransformType.Normal, 0);
        //}
    }
}
