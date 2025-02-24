using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [System.Serializable]
    public class PlayerRecord
    {
        public string playerName;
        public List<float> jumpTimes;
        public int maxScore;

        public PlayerRecord(string name)
        {
            playerName = name;
            jumpTimes = new List<float>();
            maxScore = 0;
        }
    }

    public List<PlayerRecord> players = new List<PlayerRecord>();

    public void SaveData(string playerName, float newJumpTime)
    {
        PlayerRecord player = players.Find(p => p.playerName == playerName);
        if (player == null)
        {
            player = new PlayerRecord(playerName);
            players.Add(player);
        }

        player.jumpTimes.Add(newJumpTime);
    }

    public PlayerRecord LoadData(string playerName)
    {
        return players.Find(p => p.playerName == playerName);
    }

    // get max score for all players
    public int GetMaxScore()
    {
        int maxScore = 0;
        foreach (PlayerRecord player in players)
        {
            if (player.maxScore > maxScore)
            {
                maxScore = player.maxScore;
            }
        }
        return maxScore;
    }

    // save score data by player name
    public void SaveScore(string playerName, int newScore)
    {
        PlayerRecord player = players.Find(p => p.playerName == playerName);
        if (player == null)
        {
            player = new PlayerRecord(playerName);
            players.Add(player);
        }

        if (newScore > player.maxScore)
        {
            player.maxScore = newScore;
        }
    }
}