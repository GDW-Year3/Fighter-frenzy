using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class PressToStart : MonoBehaviour
{
    [SerializeField] Text flashingText;
    [SerializeField] float timer = 0.5f;

    public SceneHandler sceneHandler;
    void Start()
    {
        StartCoroutine(BlinkText());
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            Debug.Log("Level Loaded");
            sceneHandler.OpenNextScene();
        }
    }

    IEnumerator BlinkText() 
    {
        flashingText = GetComponent<Text>();
        while (true)
        {
            // Display text and wait 0.5 seconds
            flashingText.text = "Press any button to start";
            yield return new WaitForSeconds(timer);
            //Display blank text and wait 0.5 seconds
            flashingText.text = string.Empty;
            yield return new WaitForSeconds(timer);
        }
    }
}
