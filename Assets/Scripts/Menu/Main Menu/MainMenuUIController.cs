using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUIController : MonoBehaviour
{
    public GameObject[] panels;
    public SceneHandler sceneHandler;

    public void SetActivePanel(int index)
    {
        // If we want more panels
        for (var i = 0; i < panels.Length; i++)
        {
            var active = i == index;
            var g = panels[i];
            if (g.activeSelf != active)
            {
                Debug.Log("options menu open");
                sceneHandler.FakeLoad();
                g.SetActive(active);

            }
        }
    }

    void OnEnable()
    {
        //SetActivePanel(0);
    }

    public void SetDeactivePanel(int index)
    {
        Debug.Log("options menu closed");
        sceneHandler.FakeLoad();
        panels[index].SetActive(false);
    }

    public void OnPlayGame()
    {
        //Open next scene in build index
        sceneHandler.OpenNextScene();
    }

    public void OnQuitGame()
    {
        //Quit Game
        sceneHandler.QuitGame();
    }

}
