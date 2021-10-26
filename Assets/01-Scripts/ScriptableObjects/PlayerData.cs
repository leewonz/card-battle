using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 3)]
public class PlayerData : ScriptableObject
{
    public int maxHealth;
    public int manaGainPerTurn;
    public int maxMana;
    public int drawCountPerTurn;
}
