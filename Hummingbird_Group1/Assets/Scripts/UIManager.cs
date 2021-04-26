using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public SceneController sceneController;
    //Panels

    public GameObject moreDetailPanel1;
    public GameObject moreDetailPanel2;

    public GameObject lessDetailPanel1;
    public GameObject lessDetailPanel2;

    //Cameras

    public Camera cam1;
    public Camera cam2;

    //Buttons
    
    public Button moreDetailsButt1;
    public Button moreDetailsButt2;

    public Button BothCasesButt1;
    public Button BothCasesButt2;

    public Button SwapCasesButt1;
    public Button SwapCasesButt2;

    //Camera views

    private Rect lessDetailCam1 = new Rect(0, 0, 0.25f, 1f);
    private Rect lessDetailCam2 = new Rect(0.5f, 0, 0.25f, 1f);

    private Rect moreDetailCam1 = new Rect(0, 0, 0.5f, 1f);
    private Rect moreDetailCam2 = new Rect(0, 0, 0.5f, 1f);

    private Rect hideCam = new Rect(0, 0, 0, 0);

    [Header("Case 1: More Details")]
    [SerializeField] private TextMeshProUGUI patient1MoreDetailsName;
    [SerializeField] private TextMeshProUGUI patient1MoreDetailsGender;
    [SerializeField] private TextMeshProUGUI patient1MoreDetailsAge;
    [SerializeField] private TextMeshProUGUI patient1MoreDetailsSymptoms;
    [SerializeField] private TextMeshProUGUI patient1MoreDetailsWeight;
    [SerializeField] private TextMeshProUGUI patient1MoreDetailsHeight;
    [SerializeField] private TextMeshProUGUI patient1MoreDetailsHR;
    [SerializeField] private TextMeshProUGUI patient1MoreDetailsBP;
    [SerializeField] private TextMeshProUGUI patient1MoreDetailsRR;
    [SerializeField] private TextMeshProUGUI patient1MoreDetailsTemp;
    [SerializeField] private TextMeshProUGUI patient1MoreDetailsOxygen;
    [SerializeField] private TextMeshProUGUI patient1MoreDetailsMedicine;
    //[SerializeField] private TextMeshProUGUI patient1MoreDetailsLab;

    [Header("Case 1: Less Details")]
    [SerializeField] private TextMeshProUGUI patient1LessDetailsName;
    [SerializeField] private TextMeshProUGUI patient1LessDetailsGender;
    [SerializeField] private TextMeshProUGUI patient1LessDetailsAge;
    [SerializeField] private TextMeshProUGUI patient1LessDetailsSymptoms;
    [SerializeField] private TextMeshProUGUI patient1LessDetailsHR;
    [SerializeField] private TextMeshProUGUI patient1LessDetailsBP;
    [SerializeField] private TextMeshProUGUI patient1LessDetailsRR;
    [SerializeField] private TextMeshProUGUI patient1LessDetailsOxygen;
    [SerializeField] private TextMeshProUGUI patient1LessDetailsTemp;
    [SerializeField] private TextMeshProUGUI patient1LessDetailsLab;

    [Header("Case 2: More Details")]
    [SerializeField] private TextMeshProUGUI patient2MoreDetailsName;
    [SerializeField] private TextMeshProUGUI patient2MoreDetailsGender;
    [SerializeField] private TextMeshProUGUI patient2MoreDetailsAge;
    [SerializeField] private TextMeshProUGUI patient2MoreDetailsSymptoms;
    [SerializeField] private TextMeshProUGUI patient2MoreDetailsWeight;
    [SerializeField] private TextMeshProUGUI patient2MoreDetailsHeight;
    [SerializeField] private TextMeshProUGUI patient2MoreDetailsHR;
    [SerializeField] private TextMeshProUGUI patient2MoreDetailsBP;
    [SerializeField] private TextMeshProUGUI patient2MoreDetailsRR;
    [SerializeField] private TextMeshProUGUI patient2MoreDetailsTemp;
    [SerializeField] private TextMeshProUGUI patient2MoreDetailsOxygen;
    [SerializeField] private TextMeshProUGUI patient2MoreDetailsMedicine;
    //[SerializeField] private TextMeshProUGUI patient2MoreDetailsLab;

    [Header("Case 2: Less Details")]
    [SerializeField] private TextMeshProUGUI patient2LessDetailsName;
    [SerializeField] private TextMeshProUGUI patient2LessDetailsGender;
    [SerializeField] private TextMeshProUGUI patient2LessDetailsAge;
    [SerializeField] private TextMeshProUGUI patient2LessDetailsSymptoms;
    [SerializeField] private TextMeshProUGUI patient2LessDetailsHR;
    [SerializeField] private TextMeshProUGUI patient2LessDetailsBP;
    [SerializeField] private TextMeshProUGUI patient2LessDetailsRR;
    [SerializeField] private TextMeshProUGUI patient2LessDetailsOxygen;
    [SerializeField] private TextMeshProUGUI patient2LessDetailsTemp;
    [SerializeField] private TextMeshProUGUI patient2LessDetailsLab;


    // Start is called before the first frame update
    void Start()
    {
        //Set split screen in beginning
        SetSplitScreen();

        //Add listeners to buttons
        moreDetailsButt1.onClick.AddListener(MoreDetailsClick1);
        moreDetailsButt2.onClick.AddListener(MoreDetailsClick2);

        BothCasesButt1.onClick.AddListener(SetSplitScreen);
        BothCasesButt2.onClick.AddListener(SetSplitScreen);

        SwapCasesButt1.onClick.AddListener(SwapCases1);
        SwapCasesButt2.onClick.AddListener(SwapCases2);

        StartCoroutine(PopulateCaseDetails());
    }

    public IEnumerator PopulateCaseDetails()
    {
        while (NetworkManager.instance.patientCase1.patientName == "")
        {
            yield return null;
        }

        Case case1 = NetworkManager.instance.patientCase1;

        patient1MoreDetailsName.text = case1.patientName;
        patient1MoreDetailsGender.text = case1.gender;
        patient1MoreDetailsAge.text = case1.age;
        patient1MoreDetailsSymptoms.text = case1.patientSympDesc;
        patient1MoreDetailsWeight.text = case1.weight;
        patient1MoreDetailsHeight.text = case1.height;
        patient1MoreDetailsHR.text = case1.patientHR;
        patient1MoreDetailsBP.text = case1.patientBP;
        patient1MoreDetailsRR.text = case1.patientRR;
        patient1MoreDetailsTemp.text = case1.temperature;
        patient1MoreDetailsOxygen.text = case1.patientO2Sat;
        patient1MoreDetailsMedicine.text = case1.medsTaken;
        //patient1MoreDetailsLab.text = case1;

        patient1LessDetailsName.text = case1.patientName;
        patient1LessDetailsGender.text = case1.gender;
        patient1LessDetailsAge.text = case1.age;
        patient1LessDetailsSymptoms.text = case1.patientSympDesc;
        patient1LessDetailsHR.text = case1.patientHR;
        patient1LessDetailsBP.text = case1.patientBP;
        patient1LessDetailsRR.text = case1.patientRR;
        patient1LessDetailsOxygen.text = case1.patientO2Sat;
        patient1LessDetailsTemp.text = case1.temperature;
        patient1LessDetailsLab.text = case1.lab;
        
        while (NetworkManager.instance.patientCase2.patientName == "")
        {
            yield return null;
        }

        Case case2 = NetworkManager.instance.patientCase2;

        patient2MoreDetailsName.text = case2.patientName;
        patient2MoreDetailsGender.text = case2.gender;
        patient2MoreDetailsAge.text = case2.age;
        patient2MoreDetailsSymptoms.text = case2.patientSympDesc;
        patient2MoreDetailsWeight.text = case2.weight;
        patient2MoreDetailsHeight.text = case2.height;
        patient2MoreDetailsHR.text = case2.patientHR;
        patient2MoreDetailsBP.text = case2.patientBP;
        patient2MoreDetailsRR.text = case2.patientRR;
        patient2MoreDetailsTemp.text = case2.temperature;
        patient2MoreDetailsOxygen.text = case2.patientO2Sat;
        patient2MoreDetailsMedicine.text = case2.medsTaken;
        //patient2MoreDetailsLab.text = case2;

        patient2LessDetailsName.text = case2.patientName;
        patient2LessDetailsGender.text = case2.gender;
        patient2LessDetailsAge.text = case2.age;
        patient2LessDetailsSymptoms.text = case2.patientSympDesc;
        patient2LessDetailsHR.text = case2.patientHR;
        patient2LessDetailsBP.text = case2.patientBP;
        patient2LessDetailsRR.text = case2.patientRR;
        patient2LessDetailsOxygen.text = case2.patientO2Sat;
        patient2LessDetailsTemp.text = case2.temperature;
        patient2LessDetailsLab.text = case2.lab;
    }

    void MoreDetailsClick1()
    {
        cam1.rect = moreDetailCam1;
        cam2.rect = hideCam;
        moreDetailPanel1.SetActive(true);
        moreDetailPanel2.SetActive(false);
        lessDetailPanel1.SetActive(false);
        lessDetailPanel2.SetActive(false);

    }
    void MoreDetailsClick2()
    {
        cam2.rect = moreDetailCam2;
        cam1.rect = hideCam;
        moreDetailPanel2.SetActive(true);
        moreDetailPanel1.SetActive(false);
        lessDetailPanel1.SetActive(false);
        lessDetailPanel2.SetActive(false);
    }

    void SetSplitScreen()
    {
        moreDetailPanel1.SetActive(false);
        moreDetailPanel2.SetActive(false);

        lessDetailPanel1.SetActive(true);
        lessDetailPanel2.SetActive(true);

        cam1.rect = lessDetailCam1;
        cam2.rect = lessDetailCam2;
    }

    void SwapCases1()
    {

        MoreDetailsClick2();
        
    }

    void SwapCases2()
    {

        MoreDetailsClick1();
    }

    public void ChoosePatient(int choice)
    {
        StartCoroutine(WaitForTimerToEvaluateChoice(choice));
    }

    IEnumerator WaitForTimerToEvaluateChoice(int choice)
    { 
        yield return null;
        Case case1 = NetworkManager.instance.patientCase1;
        Case case2 = NetworkManager.instance.patientCase2;
        bool won = false;
        if (choice == 1)
        {
            if (case1.patNum == 5 || 
                (case1.patNum == 4 && case2.patNum != 5) ||
                (case1.patNum == 2 && case2.patNum < 4) ||
                (case1.patNum == 1 && case2.patNum == 3))
            {
                won = true;
            }
            else
            {
                won = false;
            }
        }
        else if (choice == 2)
        {
            if (case2.patNum == 5 ||
                (case2.patNum == 4 && case1.patNum != 5) ||
                (case2.patNum == 2 && case1.patNum < 4) ||
                (case2.patNum == 1 && case1.patNum == 3))
            {
                won = true;
            }
            else
            {
                won = false;
            }
        }

        NetworkManager.instance.localPlayer.AddGameResult(won, (int)Timer.chooseTime);
        NetworkManager.instance.localPlayer.Save();
        sceneController.LoadScene("Feedback");
    }
}
