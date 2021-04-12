using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string email;
    public string nickname;

    public int numGamesPlayed;
    public int numWins;
    public List<bool> winHistory;
    public int averageDecisionTime; // in seconds
    public List<int> decisionTimeHistory; // in seconds

    //public List<int> previousGames;

    public Player(string email, string nickname, int numGamesPlayed, int numWins, List<bool> winHistory,
        int averageDecisionTime, List<int> decisionTimeHistory)
    {
        this.email = email;
        this.nickname = nickname;
        this.numGamesPlayed = numGamesPlayed;
        this.winHistory = winHistory;// new List<bool>();

        //foreach(char c in winHistory)
        //{
        //    if (c == '1')
        //    {
        //        this.winHistory.Add(true);
        //    }
        //    else if (c == '0')
        //    {
        //        this.winHistory.Add(false);
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Player's win history is corrupted. Removed incorrect character.");
        //    }
        //}

        this.averageDecisionTime = averageDecisionTime;
        this.decisionTimeHistory = decisionTimeHistory;// new List<decimal>();

        //string[] tokens = decisionTimeHistory.Split(';');
        //foreach (string s in tokens)
        //{
        //    this.decisionTimeHistory.Add(decimal.Parse(s));
        //}
    }
}
