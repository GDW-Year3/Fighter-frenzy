using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    
    [SerializeField] private int playerNumber;

    public CameraCTRL cameraCtrl;

    GameObject currentCharacterPlayer1;
    GameObject currentCharacterPlayer2;

    private void Awake()
    {
        

        if (playerNumber == 1)
        {
            currentCharacterPlayer1 = CharacterTracker.instance.getMyCharacterPlayer1();
            currentCharacterPlayer1.transform.position = this.transform.position;
            currentCharacterPlayer1.transform.rotation = this.transform.rotation;
            currentCharacterPlayer1.SetActive(true);
            currentCharacterPlayer1.layer = 6;
            currentCharacterPlayer1.tag = "Player1";

            currentCharacterPlayer1.GetComponent<Player>().Initialize();
            currentCharacterPlayer1.GetComponent<Animator>().applyRootMotion = false;

            Debug.Log("Loaded Character");

            

        }
        else if (playerNumber == 2)
        {
            currentCharacterPlayer2 = CharacterTracker.instance.getMyCharacterPlayer2();
            currentCharacterPlayer2.transform.position = this.transform.position;
            currentCharacterPlayer2.transform.rotation = this.transform.rotation;
            currentCharacterPlayer2.SetActive(true);
            currentCharacterPlayer2.layer = 7;
            currentCharacterPlayer2.tag = "Player2";

            currentCharacterPlayer2.GetComponent<Player>().Initialize();
            currentCharacterPlayer2.GetComponent<Animator>().applyRootMotion = false;


            Debug.Log("Loaded Character");

            
        }
    }

    private void Update()
    {
        

        //CharacterTracker.instance.DestroyAllPlayers();

    }


}
