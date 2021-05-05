using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * Enum to describe which piece of case information we are referring to
 */
public enum CaseData { name, gender, age, weight, height, symptoms, hr, bp, rr, temp, o2, meds, labs }

public class UIManager : MonoBehaviourPun
{
    public SceneController sceneController;

    public Case case1;
    public Case case2;

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
    [SerializeField] private TextMeshProUGUI patient1MoreDetailsLab;
    [SerializeField] private TextMeshProUGUI patient1PrognosisText;

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
    [SerializeField] private TextMeshProUGUI patient2MoreDetailsLab;
    [SerializeField] private TextMeshProUGUI patient2PrognosisText;

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
        while (NetworkManager.patientCase1.patientName == "")
        {
            yield return null;
        }

        case1 = NetworkManager.patientCase1;

        OrganSelect os1 = gameObject.GetComponent<OrganSelect>();
        os1.hummingBirdOrganNumber = case1.organNum;

        patient1MoreDetailsName.text = case1.patientName;
        patient1MoreDetailsGender.text = case1.gender;
        patient1MoreDetailsAge.text = case1.age;
        patient1MoreDetailsSymptoms.text = case1.patientSympDesc;
        patient1MoreDetailsWeight.text = case1.weight;
        patient1MoreDetailsHeight.text = case1.height;
        patient1MoreDetailsHR.text = case1.patientHR[0];
        patient1MoreDetailsBP.text = case1.patientBP[0];
        patient1MoreDetailsRR.text = case1.patientRR[0];
        patient1MoreDetailsTemp.text = case1.temperature[0];
        patient1MoreDetailsOxygen.text = case1.patientO2Sat[0];
        patient1MoreDetailsMedicine.text = case1.medsTaken;
        patient1MoreDetailsLab.text = case1.lab[0];

        patient1LessDetailsName.text = case1.patientName;
        patient1LessDetailsGender.text = case1.gender;
        patient1LessDetailsAge.text = case1.age;
        if (case1.patientSympDesc.Length > 55)
        {
            string shortDesc = case1.patientSympDesc.Substring(0, 55);
            while (shortDesc[shortDesc.Length - 1] != ' ')
            {
                shortDesc = shortDesc.Remove(shortDesc.Length - 1);
            }
            patient1LessDetailsSymptoms.text = shortDesc + "...";
        }
        else
        {
            patient1LessDetailsSymptoms.text = case1.patientSympDesc;
        }
        patient1LessDetailsHR.text = case1.patientHR[0];
        patient1LessDetailsBP.text = case1.patientBP[0];
        patient1LessDetailsRR.text = case1.patientRR[0];
        patient1LessDetailsOxygen.text = case1.patientO2Sat[0];
        patient1LessDetailsTemp.text = case1.temperature[0];
        patient1LessDetailsLab.text = case1.lab[0];

        while (NetworkManager.patientCase2.patientName == "")
        {
            yield return null;
        }

        case2 = NetworkManager.patientCase2;

        OrganSelect2 os2 = gameObject.GetComponent<OrganSelect2>();
        os2.hummingBirdOrganNumber2 = case2.organNum;

        patient2MoreDetailsName.text = case2.patientName;
        patient2MoreDetailsGender.text = case2.gender;
        patient2MoreDetailsAge.text = case2.age;
        patient2MoreDetailsSymptoms.text = case2.patientSympDesc;
        patient2MoreDetailsWeight.text = case2.weight;
        patient2MoreDetailsHeight.text = case2.height;
        patient2MoreDetailsHR.text = case2.patientHR[0];
        patient2MoreDetailsBP.text = case2.patientBP[0];
        patient2MoreDetailsRR.text = case2.patientRR[0];
        patient2MoreDetailsTemp.text = case2.temperature[0];
        patient2MoreDetailsOxygen.text = case2.patientO2Sat[0];
        patient2MoreDetailsMedicine.text = case2.medsTaken;
        patient2MoreDetailsLab.text = case2.lab[0];

        patient2LessDetailsName.text = case2.patientName;
        patient2LessDetailsGender.text = case2.gender;
        patient2LessDetailsAge.text = case2.age;
        patient2LessDetailsSymptoms.text = case2.patientSympDesc;
        patient2LessDetailsHR.text = case2.patientHR[0];
        patient2LessDetailsBP.text = case2.patientBP[0];
        patient2LessDetailsRR.text = case2.patientRR[0];
        patient2LessDetailsOxygen.text = case2.patientO2Sat[0];
        patient2LessDetailsTemp.text = case2.temperature[0];
        patient2LessDetailsLab.text = case2.lab[0];
    }

    public void OnCase1SliderChange(Slider slider)
    {
        patient1PrognosisText.text = "Prognosis: ";
        if (slider.value < 0.2f)
        {
            patient1PrognosisText.text += "Now";
            patient1MoreDetailsHR.text = case1.patientHR[0];
            patient1MoreDetailsBP.text = case1.patientBP[0];
            patient1MoreDetailsRR.text = case1.patientRR[0];
            patient1MoreDetailsTemp.text = case1.temperature[0];
            patient1MoreDetailsOxygen.text = case1.patientO2Sat[0];
            patient1MoreDetailsLab.text = case1.lab[0];
        }
        else if (slider.value < 0.5f)
        {
            patient1PrognosisText.text += "+1 hour";
            patient1MoreDetailsHR.text = case1.patientHR[1];
            patient1MoreDetailsBP.text = case1.patientBP[1];
            patient1MoreDetailsRR.text = case1.patientRR[1];
            patient1MoreDetailsTemp.text = case1.temperature[1];
            patient1MoreDetailsOxygen.text = case1.patientO2Sat[1];
            patient1MoreDetailsLab.text = case1.lab[1];
        }
        else if (slider.value < 0.75)
        {
            patient1PrognosisText.text += "+2 hours";
            patient1MoreDetailsHR.text = case1.patientHR[2];
            patient1MoreDetailsBP.text = case1.patientBP[2];
            patient1MoreDetailsRR.text = case1.patientRR[2];
            patient1MoreDetailsTemp.text = case1.temperature[2];
            patient1MoreDetailsOxygen.text = case1.patientO2Sat[2];
            patient1MoreDetailsLab.text = case1.lab[2];
        }
        else
        {
            patient1PrognosisText.text += "+24 hours";
            patient1MoreDetailsHR.text = case1.patientHR[3];
            patient1MoreDetailsBP.text = case1.patientBP[3];
            patient1MoreDetailsRR.text = case1.patientRR[3];
            patient1MoreDetailsTemp.text = case1.temperature[3];
            patient1MoreDetailsOxygen.text = case1.patientO2Sat[3];
            patient1MoreDetailsLab.text = case1.lab[3];
        }
    }

    public void OnCase2SliderChange(Slider slider)
    {
        patient2PrognosisText.text = "Prognosis: ";
        if (slider.value < 0.2f)
        {
            patient2PrognosisText.text += "Now";
            patient2MoreDetailsHR.text = case2.patientHR[0];
            patient2MoreDetailsBP.text = case2.patientBP[0];
            patient2MoreDetailsRR.text = case2.patientRR[0];
            patient2MoreDetailsTemp.text = case2.temperature[0];
            patient2MoreDetailsOxygen.text = case2.patientO2Sat[0];
            patient2MoreDetailsLab.text = case2.lab[0];
        }
        else if (slider.value < 0.5f)
        {
            patient2PrognosisText.text += "+1 hour";
            patient2MoreDetailsHR.text = case2.patientHR[1];
            patient2MoreDetailsBP.text = case2.patientBP[1];
            patient2MoreDetailsRR.text = case2.patientRR[1];
            patient2MoreDetailsTemp.text = case2.temperature[1];
            patient2MoreDetailsOxygen.text = case2.patientO2Sat[1];
            patient2MoreDetailsLab.text = case2.lab[1];
        }
        else if (slider.value < 0.75)
        {
            patient2PrognosisText.text += "+2 hours";
            patient2MoreDetailsHR.text = case2.patientHR[2];
            patient2MoreDetailsBP.text = case2.patientBP[2];
            patient2MoreDetailsRR.text = case2.patientRR[2];
            patient2MoreDetailsTemp.text = case2.temperature[2];
            patient2MoreDetailsOxygen.text = case2.patientO2Sat[2];
            patient2MoreDetailsLab.text = case2.lab[2];
        }
        else
        {
            patient2PrognosisText.text += "+24 hours";
            patient2MoreDetailsHR.text = case2.patientHR[3];
            patient2MoreDetailsBP.text = case2.patientBP[3];
            patient2MoreDetailsRR.text = case2.patientRR[3];
            patient2MoreDetailsTemp.text = case2.temperature[3];
            patient2MoreDetailsOxygen.text = case2.patientO2Sat[3];
            patient2MoreDetailsLab.text = case2.lab[3];
        }
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

        NetworkManager.localPlayer.AddGameResult(won, (int)Timer.chooseTime);
        NetworkManager.localPlayer.Save();
        NetworkManager.instance.LeaveRoom();
        sceneController.LoadScene("Feedback");
    }
}