using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNav : MonoBehaviour
{
    public int i;

    public void Quit()
    {
        Application.Quit();
    }

    // Handles Scene Changes
    public void MenuNavigator()
    {
        SceneManager.LoadScene(i, LoadSceneMode.Single);
    }
}
