using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private CharacterSelectButton buttonPrefab;
    [SerializeField] private Transform contents;
    //[SerializeField] private GameObject myCharacter;

    //Buttons
    [SerializeField] private GameObject player1ConfirmButton;
    [SerializeField] private GameObject player2ConfirmButton;
    [SerializeField] private GameObject FinalConfirmButton;


    //[SerializeField] private GameObject currentCharacterPlayer1;
    //[SerializeField] private GameObject currentCharacterPlayer2;

    [SerializeField] private Transform player1SpawnPoint;
    [SerializeField] private Transform player2SpawnPoint;



    [SerializeField] public GameObject[] characters;

    [SerializeField] private int playerNumber;


    //public static CharacterSelection instance;

    GameObject CharacterPlayer1Showcase;
    GameObject CharacterPlayer2Showcase;

    

    //GameObject showcaseCharacter1;



    List<CharacterSelectButton> characterButtonList = new List<CharacterSelectButton>();
    /*
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
    */

    // Start is called before the first frame update
    void Start()
    {
        playerNumber = 1;
        player1ConfirmButton.SetActive(true);
        player2ConfirmButton.SetActive(false);
        FinalConfirmButton.SetActive(false);

        //CharacterPlayer1Showcase = currentCharacterPlayer1;
        //CharacterPlayer2Showcase = currentCharacterPlayer2;

        for (int i = 0; i < characters.Length; i++)
        {
            CharacterSelectButton spawnedPrefab = Instantiate(buttonPrefab, contents);
            spawnedPrefab.Intialize(characters[i]);
            characterButtonList.Add(spawnedPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLoadCharacter(GameObject loadedCharacter)
    {
        //Instantiate(myCharacter, currentCharacter.transform.position, Quaternion.identity);
        //Destroy(currentCharacter);  

        if(CharacterPlayer1Showcase != loadedCharacter && playerNumber == 1)
        {
            //DestroyImmediate(currentCharacterPlayer1, true);
            Destroy(CharacterPlayer1Showcase);
            //currentCharacterPlayer1 = CharacterPlayer1Showcase;
        }
        else if (CharacterPlayer2Showcase != loadedCharacter && playerNumber == 2)
        {
            Destroy(CharacterPlayer2Showcase);
        }

        if (playerNumber == 1)
        {

            CharacterPlayer1Showcase = Instantiate(loadedCharacter, player1SpawnPoint.position, Quaternion.identity);
            //showcaseCharacter1.SetActive(true);

            //currentCharacterPlayer1 = Instantiate(currentCharacter, CharacterPlayer1Showcase.transform.position, Quaternion.identity);
            //currentCharacterPlayer1.SetActive(true);

            //currentCharacterPlayer1 = currentCharacter;

            //Debug.Log("Loaded Character");
        }
        else if(playerNumber == 2)
        {

            CharacterPlayer2Showcase = Instantiate(loadedCharacter, player2SpawnPoint.position, Quaternion.identity);




            //Debug.Log("Loaded Character");
        }

        //GameObject currentCharacter;
        //currentCharacter = myCharacter;
        //currentCharacter = Instantiate(myCharacter, PlayerPedestal.transform.position + new Vector3(0, Offset, 0), Quaternion.identity) as GameObject;
        //currentCharacter.SetActive(myCharacter);
    }

    public void OnConfirmPlayer()
    {
        if(playerNumber == 1)
        {
            player1ConfirmButton.SetActive(false);
            player2ConfirmButton.SetActive(true);



            playerNumber = 2;
        }
        else if(playerNumber == 2)
        {
            player2ConfirmButton.SetActive(false);
            FinalConfirmButton.SetActive(true);

            for (int i = 0; i < characterButtonList.Count; i++)
            {
                characterButtonList[i].gameObject.SetActive(false);
            }

            playerNumber = 3;

            

        }
        else if(playerNumber == 3)
        {
            //Confirm players and move on to next scene
            //currentCharacterPlayer1
            //currentCharacterPlayer2

            UserInput userInput1 = CharacterPlayer1Showcase.GetComponent<UserInput>();
            UserInput userInput2 = CharacterPlayer2Showcase.GetComponent<UserInput>();

            userInput1.setMyID(0);
            userInput2.setMyID(1);

            CharacterTracker.instance.setMyCharacterPlayer1(CharacterPlayer1Showcase);
            CharacterTracker.instance.setMyCharacterPlayer2(CharacterPlayer2Showcase);



            // load the next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }



}
