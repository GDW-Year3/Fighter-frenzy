using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;
public class EndScreen : MonoBehaviour
{
    private Player m_p1;
    private Player m_p2;
    private CharacterLoader[] m_Ls;
    private void FindPlayers()
    {
        m_p1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        m_p2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();
        m_Ls = FindObjectsOfType<CharacterLoader>();
    }
    private void Update()
    {
        if (m_p1 == null || m_p2 == null) FindPlayers();
    }
    public void PlayAgainWithSameCharacters()
    {
        Playagin();
        DestroyPlayers();
        SceneManager.LoadScene(3);
    }
    private void Playagin()
    {
        GameObject p1 = Instantiate(m_p1.gameObject, Vector3.zero, Quaternion.identity);
        p1.GetComponent<UserInput>().setMyID(0);
        CharacterTracker.instance.setMyCharacterPlayer1(p1);
        GameObject p2 = Instantiate(m_p2.gameObject, Vector3.zero, Quaternion.identity);
        p2.GetComponent<UserInput>().setMyID(1);
        CharacterTracker.instance.setMyCharacterPlayer2(p2);
    }
    public void PlayAgainWithNewCharacters()
    {
        DestroyPlayers();
        foreach (CharacterLoader c in m_Ls)
        {
            c.ran = false;
        }
        SceneManager.LoadScene(2);
    }
    public void QuitToMainMenu()
    {
        DestroyPlayers();
        foreach (CharacterLoader c in m_Ls)
        {
            c.ran = false;
        }
        SceneManager.LoadScene(1);
    }
    private void DestroyPlayers()
    {
        Destroy(m_p1.gameObject);
        Destroy(m_p2.gameObject);
    }
}
