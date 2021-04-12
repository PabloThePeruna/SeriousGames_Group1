using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ResultManager : MonoBehaviour
{
    //String to show results in Feedback-scene
    public TextMeshProUGUI resultText;

    // Start is called before the first frame update
    void Start()
    {
        //Show the chooseTime in result screen
        resultText.SetText("You're choosing time were " + Timer.chooseTime + " seconds.");
    }


}
