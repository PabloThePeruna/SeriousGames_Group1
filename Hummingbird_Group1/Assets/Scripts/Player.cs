using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player
{
    public string userID; // databse user id
    public string email; // email to login
    public string nickname; // nickname to display

    public int numGamesPlayed; // number of games the player has completed
    public int numWins; // the number of times the player has won
    public List<bool> winHistory; // the full win history (true == win, false == lose)
    public List<int> decisionTimeHistory; // in seconds, the time taken to make a decision for each game completed

    public Player(string userID, string email, string nickname, int numGamesPlayed, int numWins, List<bool> winHistory, List<int> decisionTimeHistory)
    {
        this.userID = userID;
        this.email = email;
        this.nickname = nickname;
        this.numGamesPlayed = numGamesPlayed;
        this.winHistory = winHistory;
        this.decisionTimeHistory = decisionTimeHistory;

  
    }

    /*
     * Get a list of the average decision times based on the player's history
     */
    public List<int> GetAverageTimeHistory()
    {
        List<int> avg = new List<int>();
        avg.Add(decisionTimeHistory[0]);

        for (int i = 1; i < decisionTimeHistory.Count; i++)
        {
            float fraction = 1f / (i + 1);
            int nextAvg = (int)((fraction * i * avg[i - 1]) + (fraction * decisionTimeHistory[i]));
            avg.Add(nextAvg);
        }

        return avg;
    }

    /*
     * Update the player with a new result
     */
    public void AddGameResult(bool won, int timeInSeconds)
    {
        if (numGamesPlayed == 0)
        {
            winHistory = new List<bool>();
            decisionTimeHistory = new List<int>();
        }

        numGamesPlayed++;
        if (won)
        {
            numWins++;
        }

        winHistory.Add(won);
        decisionTimeHistory.Add(timeInSeconds);
    }

    public void Save()
    {
        Database.PostPlayerToDatabase(this, PostCallback);
    }

    public void PostCallback(bool success)
    {
        Debug.Log("Player post result: " + success);
    }
}
