using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

public static class Database
{
    public static string databaseURL = "https://icarus-firebase-rdb-default-rtdb.firebaseio.com/";

    public delegate void PostCaseCallback();
    public delegate void RetrieveCaseCallback(Case patientCase);
    public delegate void PostPlayerCallback();
    public delegate void RetrievePlayerCallback(Player player);

    public static void PostCaseToDatabase(Case patientCase, PostCaseCallback callback)
    {
        RestClient.Put<Case>(databaseURL + patientCase.patientName + ".json", patientCase).Then(response =>
        {
            callback();
        });
    }

    public static void RetrieveCaseFromDatabase(string patientName, RetrieveCaseCallback callback)
    {
        RestClient.Get<Case>(databaseURL + patientName + ".json").Then(response =>
        {
            callback(response);
        });
    }
    public static void PostPlayerToDatabase(Player player, PostPlayerCallback callback)
    {
        RestClient.Put<Player>(databaseURL + player.email + ".json", player).Then(response =>
        {
            callback();
        });
    }

    public static void RetrievePlayerFromDatabase(string email, RetrievePlayerCallback callback)
    {
        RestClient.Get<Player>(databaseURL + email + ".json").Then(response =>
        {
            callback(response);
        });
    }
}
