using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LogOut()
    {
        if (NetworkManager.instance != null)
        {
            NetworkManager.instance.LogOut();
        }
        SceneManager.LoadScene(0);
    }

}
