using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenSpecificCase : MonoBehaviour
{
    [SerializeField] private GameObject canvas2;
    public Button SeeMoreDetails2;
    public static bool IsCanvasChanged = false;

    //For opening right part of game menu from feedack scene
    [SerializeField] private GameObject home;
    [SerializeField] private GameObject multip_start;
    [SerializeField] private GameObject multip_room;

    void Start()
    {
        SeeMoreDetails2.onClick.AddListener(OpenCase);
        canvas2 = GameObject.FindGameObjectWithTag("canvas2");

    }

    public void Update()
    {
        if (IsCanvasChanged)
        {
            canvas2.SetActive(true);

            home.SetActive(false);
            multip_start.SetActive(false);

        }
        IsCanvasChanged = false;

    }

    public void OpenCase()
    {
        IsCanvasChanged = true;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
