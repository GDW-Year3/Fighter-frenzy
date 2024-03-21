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


    [SerializeField] private GameObject currentCharacterPlayer1;
    [SerializeField] private GameObject currentCharacterPlayer2;



    [SerializeField] public GameObject[] characters;

    [SerializeField] private int playerNumber;


    //public static CharacterSelection instance;

    GameObject CharacterPlayer1Showcase;
    GameObject CharacterPlayer2Showcase;



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

        CharacterPlayer1Showcase = currentCharacterPlayer1;
        CharacterPlayer2Showcase = currentCharacterPlayer2;

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

    public void OnLoadCharacter(GameObject currentCharacter)
    {
        //Instantiate(myCharacter, currentCharacter.transform.position, Quaternion.identity);
        //Destroy(currentCharacter);  

        if(currentCharacterPlayer1 != CharacterPlayer1Showcase && playerNumber == 1)
        {
            Destroy(currentCharacterPlayer1);
            currentCharacterPlayer1 = CharacterPlayer1Showcase;
        }
        else if (currentCharacterPlayer2 != CharacterPlayer2Showcase && playerNumber == 2)
        {
            Destroy(currentCharacterPlayer2);
            currentCharacterPlayer2 = CharacterPlayer2Showcase;
        }

        if (playerNumber == 1)
        {

            GameObject showcaseCharacter1 = Instantiate(currentCharacter, CharacterPlayer1Showcase.transform.position, Quaternion.identity);
            showcaseCharacter1.SetActive(true);

            //currentCharacterPlayer1 = Instantiate(currentCharacter, CharacterPlayer1Showcase.transform.position, Quaternion.identity);
            //currentCharacterPlayer1.SetActive(true);

            currentCharacterPlayer1 = currentCharacter;

            //Debug.Log("Loaded Character");
        }
        else if(playerNumber == 2)
        {

            GameObject showcaseCharacter2 = Instantiate(currentCharacter, CharacterPlayer2Showcase.transform.position, Quaternion.identity);
            showcaseCharacter2.SetActive(true);

            //currentCharacterPlayer2 = Instantiate(currentCharacter, CharacterPlayer2Showcase.transform.position, Quaternion.identity);
            //currentCharacterPlayer2.SetActive(true);

            currentCharacterPlayer2 = currentCharacter;

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

            UserInput userInput1 = currentCharacterPlayer1.GetComponent<UserInput>();
            UserInput userInput2 = currentCharacterPlayer2.GetComponent<UserInput>();

            userInput1.setMyID(0);
            userInput2.setMyID(1);

            CharacterTracker.instance.setMyCharacterPlayer1(currentCharacterPlayer1);
            CharacterTracker.instance.setMyCharacterPlayer2(currentCharacterPlayer2);



            // load the next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }



}
