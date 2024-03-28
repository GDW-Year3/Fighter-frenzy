using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterTracker : MonoBehaviour
{
    private GameObject characterPlayer1;
    private GameObject characterPlayer2;

    public static CharacterTracker instance;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }



    public void setMyCharacterPlayer1(GameObject myCharacterPlayer1)
    {
        characterPlayer1 = myCharacterPlayer1;
    }

    public void setMyCharacterPlayer2(GameObject myCharacterPlayer2)
    {
        characterPlayer2 = myCharacterPlayer2;
    }

    public GameObject getMyCharacterPlayer1()
    {
        return characterPlayer1;
    }

    public GameObject getMyCharacterPlayer2()
    {
        return characterPlayer2;
    }
}
