using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class User
{
    public string patientName;
    public string patientRR;
    public string patientBP;
    public string patientO2Sat;
    public string patientHR;
    public string patientSympDesc;
    public string patientPatDesc;
    public string lab;
    public string medsTaken;
    public int patNum;

    public User()
    {
        patientName = PlayerScores.playerName;
        patientRR = PlayerScores.rR;
        patientBP = PlayerScores.bP;
        patientO2Sat = PlayerScores.o2Sat;
        patientHR = PlayerScores.hR;
        patientSympDesc = PlayerScores.sympDesc;
        patientPatDesc = PlayerScores.patDesc;
        lab = PlayerScores.labRes;
        medsTaken = PlayerScores.meds;
        patNum = PlayerScores.patientNumber;
    }


}
