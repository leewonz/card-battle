using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CameraTransformHolder : MonoBehaviour
{
    [Serializable]
    public enum CameraTransformType
    {
        Normal,
        PlayerSlot,
        PlayerAll,
        EnemySlot,
        EnemyAll,
        BattleStart
    }

    [Serializable]
    public struct CameraTransformData
    {
        public Transform transform;
        public CameraTransformType type;
        public int num;
    }

    public List<CameraTransformData> list;

    public Transform GetTransform(CameraTransformType type, int num)
    {
        foreach(var transformData in list)
        {
            if (transformData.type == type &&
                transformData.num == num)
            {
                return transformData.transform;
            }
        }
        return null;
    }

    public Transform GetTransform(Team team)
    {
        foreach (var transformData in list)
        {
            switch (team)
            {
                case Team.Player:
                    if (transformData.type == CameraTransformType.PlayerAll)
                    {
                        return transformData.transform;
                    }
                    break;
                case Team.Opponent:
                    if (transformData.type == CameraTransformType.EnemyAll)
                    {
                        return transformData.transform;
                    }
                    break;
            }
        }
        return null;
    }

    public Transform GetTransform(Team team, int num)
    {
        foreach (var transformData in list)
        {
            switch (team)
            {
                case Team.Player:
                    if (transformData.type == CameraTransformType.PlayerSlot &&
                        transformData.num == num)
                    {
                        return transformData.transform;
                    }
                    break;
                case Team.Opponent:
                    if (transformData.type == CameraTransformType.EnemySlot &&
                        transformData.num == num)
                    {
                        return transformData.transform;
                    }
                    break;
            }
        }
        return null;
    }
}
