using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndScreen : MonoBehaviour
{
    public void PlayAgainWithSameCharacters()
    {
        SceneManager.LoadScene(3);
    }
    public void PlayAgainWithNewCharacters()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
