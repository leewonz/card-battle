using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team { Player, Opponent, All};

public class DataManager : Singleton<DataManager>
{
    public static PlayerCards GetPlayerCards(Team team)
    {
        PlayerCards playerCards;
        switch (team)
        {
            case Team.Player:
                playerCards = GameObject.FindGameObjectWithTag(Con.Tags.player).GetComponent<PlayerCards>();
                break;
            case Team.Opponent:
                playerCards = GameObject.FindGameObjectWithTag(Con.Tags.enemy).GetComponent<PlayerCards>();
                break;
            default:
                playerCards = null;
                break;
        }
        return playerCards;
    }

    public static Team OppositeTeam(Team team)
    {
        switch(team)
        {
            case Team.Player:
                return Team.Opponent;
            case Team.Opponent:
                return Team.Player;
            default:
                return Team.All;
        }
    }
}
