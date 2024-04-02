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
            currentCharacterPlayer1.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
            currentCharacterPlayer1.SetActive(true);
            foreach (Transform t in GetAllChildren(currentCharacterPlayer1.transform))
            {
                t.gameObject.layer = 6;
            }
            currentCharacterPlayer1.layer = 6;
            currentCharacterPlayer1.tag = "Player1";

            currentCharacterPlayer1.GetComponent<Player>().Initialize();
            currentCharacterPlayer1.GetComponent<Animator>().applyRootMotion = false;

            Debug.Log("Loaded Character");
        }
        else if (playerNumber == 2)
        {
            currentCharacterPlayer2 = CharacterTracker.instance.getMyCharacterPlayer2();
            currentCharacterPlayer2.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
            currentCharacterPlayer2.SetActive(true);
            foreach (Transform t in GetAllChildren(currentCharacterPlayer2.transform))
            {
                t.gameObject.layer = 7;
            }
            currentCharacterPlayer2.layer = 7;
            currentCharacterPlayer2.tag = "Player2";

            currentCharacterPlayer2.GetComponent<Player>().Initialize();
            currentCharacterPlayer2.GetComponent<Animator>().applyRootMotion = false;
            Debug.Log("Loaded Character");
        }
    }
    public static List<Transform> GetAllChildren(Transform parent)
    {
        List<Transform> children = new List<Transform>();
        foreach (Transform child in parent)
        {
            children.Add(child);
            children.AddRange(GetAllChildren(child)); // Recursive call to get sub-children
        }
        return children;
    }
}
