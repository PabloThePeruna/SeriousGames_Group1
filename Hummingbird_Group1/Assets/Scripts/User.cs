using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class User
{
    public string userName;
    public string patientRR;
    public string patientBP;
    public string patientO2Sat;
    public string patientHR;
    public string patientSympDesc;
    public string patientPatDesc;

    public User()
    {
        userName = PlayerScores.playerName;
        patientRR = PlayerScores.rR;
        patientBP = PlayerScores.bP;
        patientO2Sat = PlayerScores.o2Sat;
        patientHR = PlayerScores.hR;
        patientSympDesc = PlayerScores.sympDesc;
        patientPatDesc = PlayerScores.patDesc;
    }


}
