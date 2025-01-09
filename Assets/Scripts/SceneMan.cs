using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneMan : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Start new game!");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit game!");
    }

}
