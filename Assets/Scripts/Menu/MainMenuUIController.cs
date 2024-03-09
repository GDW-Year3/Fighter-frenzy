using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    public GameObject[] panels;

    public void SetActivePanel(int index)
    {
        // If we want more panels
        for (var i = 0; i < panels.Length; i++)
        {
            var active = i == index;
            var g = panels[i];
            if (g.activeSelf != active) g.SetActive(active);
        }
    }

    void OnEnable()
    {
        //SetActivePanel(0);
    }

    public void SetDeactivePanel(int index)
    {
        panels[index].SetActive(false);
    }


    public void OnPlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }
}
