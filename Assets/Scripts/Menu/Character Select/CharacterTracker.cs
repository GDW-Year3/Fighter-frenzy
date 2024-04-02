using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterTracker : MonoBehaviour
{
    [SerializeField] private GameObject characterPlayer1;
    [SerializeField] private GameObject characterPlayer2;


    public static CharacterTracker instance;

    bool yep = false;


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


    private void Update()
    {

        if (characterPlayer1 != null && characterPlayer2 != null && yep == false)
        {
            DontDestroyOnLoad(characterPlayer1);
            DontDestroyOnLoad(characterPlayer2);
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
