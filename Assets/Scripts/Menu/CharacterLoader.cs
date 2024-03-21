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
            currentCharacterPlayer1 = Instantiate(CharacterTracker.instance.getMyCharacterPlayer1(), this.transform.position, this.transform.rotation);
            currentCharacterPlayer1.SetActive(true);
            currentCharacterPlayer1.layer = 6;
            currentCharacterPlayer1.tag = "Player1";


            Debug.Log("Loaded Character");

            

        }
        else if (playerNumber == 2)
        {
            currentCharacterPlayer2 = Instantiate(CharacterTracker.instance.getMyCharacterPlayer2(), this.transform.position, this.transform.rotation);
            currentCharacterPlayer2.SetActive(true);
            currentCharacterPlayer2.layer = 7;
            currentCharacterPlayer2.tag = "Player2";
            Debug.Log("Loaded Character");

            
        }
    }

    private void Update()
    {
        

        //CharacterTracker.instance.DestroyAllPlayers();

    }


}
