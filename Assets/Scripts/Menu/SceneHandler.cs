using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] RectTransform fader;

    //Animate Loading Screen
    private void Start()
    {
        //Enable fader
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            fader.gameObject.SetActive(false);
        });
    }


    public void OpenNextScene() 
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, Vector3.one, 0.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }

    public void QuitGame()
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, Vector3.one, 0.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            Application.Quit();
        });
    }

    public void FakeLoad()
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, Vector3.one, 0.5f).setEase(LeanTweenType.easeInOutExpo);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            fader.gameObject.SetActive(false);
        });

    }
}
