using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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

    }

    // Update is called once per frame
    void Update()
    {
        
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
}
