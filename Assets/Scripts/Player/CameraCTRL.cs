using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CameraCTRL : MonoBehaviour
{
    [SerializeField] private float m_targRotSpeed;
    [SerializeField] private float m_camRotSpeed;
    [SerializeField] private float m_camMoveSpeed;
    [SerializeField] private float m_playerRotSpeed;
    [SerializeField] private float m_playerRepelSpeed;
    [SerializeField] private float m_playerMinDist;
    [SerializeField] private Player m_p1;
    [SerializeField] private Player m_p2;
    [SerializeField] private GameObject m_endScreen;
    [SerializeField] private TextMeshProUGUI m_endText;
    private void Update()
    {
        m_p1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        m_p2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();


        if (!m_p1.IsDead) m_p1.transform.forward = (m_p2.transform.position - m_p1.transform.position).normalized;
        if (!m_p2.IsDead) m_p2.transform.forward = (m_p1.transform.position - m_p2.transform.position).normalized;

        if (Vector3.Distance(m_p1.transform.position, m_p2.transform.position) < m_playerMinDist)
        {
            m_p1.RB.AddForce(-m_playerRepelSpeed * m_p1.transform.forward);
            m_p2.RB.AddForce(-m_playerRepelSpeed * m_p2.transform.forward);
        }

        transform.SetPositionAndRotation(Vector3.Lerp(m_p1.transform.position, m_p2.transform.position, 0.5f), Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_p1.transform.forward, Vector3.up), m_targRotSpeed * Time.deltaTime));
        //m_target.SetPositionAndRotation(Vector3.Lerp(p1.position, p2.position, 0.5f), Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(p1.forward, Vector3.up), m_targRotSpeed * Time.deltaTime));
        //transform.SetPositionAndRotation(Vector3.Lerp(transform.position, m_target.position, m_camMoveSpeed * Time.deltaTime), Quaternion.Slerp(transform.rotation, m_target.rotation, m_camRotSpeed * Time.deltaTime));
        if (m_p1.HP <= 0f) HandleEndScreen(1);
        if (m_p2.HP <= 0f) HandleEndScreen(2);
    }
    private void HandleEndScreen(int id)
    {
        m_endScreen.SetActive(true);
        m_endText.text = ($"Player {id} WINS!");
    }
}
