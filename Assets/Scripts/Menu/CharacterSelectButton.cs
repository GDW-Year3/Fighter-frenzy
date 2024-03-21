using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CharacterSelectButton : MonoBehaviour
{
    [SerializeField] private Image thumbnailImage;
    //[SerializeField] private GameObject PlayerPedestal;
    //[SerializeField] private CharacterSelection Characters;
    //[SerializeField] private GameObject myCharacter;
    //[SerializeField] private GameObject currentCharacter;


    //[SerializeField] private float Offset;

    public CharacterSelection CharacterSelection;

    public GameObject myCharacter;

    //public int characterID;

    void Start()
    {
        CharacterSelection = FindAnyObjectByType<CharacterSelection>();
        //currentCharacter = null;
    }

    public void Intialize(GameObject newCharacter)
    {
        //Texture2D texture = AssetPreview.GetMiniThumbnail(myCharacter);
        //thumbnailImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.width), new Vector2(0.5f,0.5f));
        myCharacter = newCharacter;
        //characterID = ID;

    }


    public void OnClick()
    {

        CharacterSelection.OnLoadCharacter(myCharacter);

        //Instantiate(myCharacter, currentCharacter.transform.position, Quaternion.identity);
        //Destroy(currentCharacter);


        //GameObject currentCharacter;
        //currentCharacter = myCharacter;
        //currentCharacter = Instantiate(myCharacter, PlayerPedestal.transform.position + new Vector3(0, Offset, 0), Quaternion.identity) as GameObject;
        //currentCharacter.SetActive(myCharacter);


        // tell character tracker the correct file path
        //CharacterTracker.instance.setMyCharacter(myCharacter);

        // load the next scene
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
