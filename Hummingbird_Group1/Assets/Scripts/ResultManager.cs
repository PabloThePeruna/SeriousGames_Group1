using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;


public class ResultManager : MonoBehaviour
{
    private Player player;
    //String to show results in Feedback-scene
    public TextMeshProUGUI resultTextMin;
    public TextMeshProUGUI resultTextSec;

    public TextMeshProUGUI importantTextCase1;
    public TextMeshProUGUI hedder;
    
    int timer;

    //Change the player name to a heading
    public TextMeshProUGUI username;
  

    // Start is called before the first frame update

    private void OnEnable()
    {
        player = NetworkManager.localPlayer;
        timer = player.decisionTimeHistory[player.decisionTimeHistory.Count - 1];
    }
    void Start()
    {
        username.SetText(NetworkManager.localPlayer.nickname);

        int minutes = timer / 60;
        int seconds = timer % 60;
        //Show the chooseTime in result screen
        resultTextMin.SetText(minutes.ToString());
        resultTextSec.SetText(seconds.ToString());

        Debug.Log(NetworkManager.instance.patientCase1.patientName);
     
        //if(NetworkManager.instance.patientCase1.patNum!= 1 || NetworkManager.instance.patientCase1.patNum != 2 || NetworkManager.instance.patientCase1.patNum != 4)
        //{
        //    importantTextCase1.SetText("Respiration Rate 40, Saturation 80% without oxygen ,White cell count 40 CRP 250 ");
        //}
        if (NetworkManager.instance.patientCase2.patNum ==4 && NetworkManager.instance.patientCase1.patNum==5|| NetworkManager.instance.patientCase2.patNum == 5 && NetworkManager.instance.patientCase1.patNum == 4)
        {
            importantTextCase1.SetText("Respiration Rate 40, Saturation 80% without oxygen ,White cell count 40 CRP 250 ");
        }
        //if (NetworkManager.instance.patientCase1.patNum != 5 || NetworkManager.instance.patientCase1.patNum != 2 || NetworkManager.instance.patientCase1.patNum != 1)
        //{
        //    importantTextCase1.SetText("Respiration Rate 32, Saturation 90% without oxygen");
        //}
        //if (NetworkManager.instance.patientCase2.patNum != 5 || NetworkManager.instance.patientCase2.patNum != 2 || NetworkManager.instance.patientCase2.patNum != 1)
        //{
        //    importantTextCase1.SetText("Respiration Rate 32, Saturation 90% without oxygen");
        //}

        if (NetworkManager.instance.patientCase2.patNum == 4 && NetworkManager.instance.patientCase1.patNum == 3|| NetworkManager.instance.patientCase2.patNum == 3 && NetworkManager.instance.patientCase1.patNum == 4)
        {
            importantTextCase1.SetText("Respiration Rate 32, Saturation 90% without oxygen ");
        }


        if (NetworkManager.instance.patientCase2.patNum == 2 && NetworkManager.instance.patientCase1.patNum == 4 || NetworkManager.instance.patientCase2.patNum == 4 && NetworkManager.instance.patientCase1.patNum == 2)
        {
            importantTextCase1.SetText("Respiration Rate 32, Saturation 90% without oxygen ");
        }
        if (NetworkManager.instance.patientCase2.patNum == 2 && NetworkManager.instance.patientCase1.patNum == 5 || NetworkManager.instance.patientCase2.patNum == 5 && NetworkManager.instance.patientCase1.patNum == 2)
        {
            importantTextCase1.SetText("Respiration Rate 40, Saturation 80% without oxygen ,White cell count 40 CRP 250 ");
        }
        if (player.winHistory.Last() == false)
        {
            hedder.SetText("You choose wrong!");
        }
        if (player.winHistory.Last() == true)
        {
            hedder.SetText("You choose correct!");
        }

    }


}
