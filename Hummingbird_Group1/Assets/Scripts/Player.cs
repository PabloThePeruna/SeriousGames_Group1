using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string userID;
    public string email;
    public string nickname;

    public int numGamesPlayed;
    public int numWins;
    public List<bool> winHistory;
    public List<int> decisionTimeHistory; // in seconds

    //public List<int> previousGames;

    public Player(string userID, string email, string nickname, int numGamesPlayed, int numWins, List<bool> winHistory, List<int> decisionTimeHistory)
    {
        this.userID = userID;
        this.email = email;
        this.nickname = nickname;
        this.numGamesPlayed = numGamesPlayed;
        this.winHistory = winHistory;// new List<bool>();
        this.decisionTimeHistory = decisionTimeHistory;// new List<decimal>();
    }
}
