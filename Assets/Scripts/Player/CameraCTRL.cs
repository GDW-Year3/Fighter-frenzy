using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCTRL : MonoBehaviour
{
    [SerializeField] private float m_targRotSpeed;
    [SerializeField] private float m_camRotSpeed;
    [SerializeField] private float m_camMoveSpeed;
    [SerializeField] private float m_playerRotSpeed;
    [SerializeField] private float m_playerRepelSpeed;
    [SerializeField] private float m_playerMinDist;
    [SerializeField] private Player p1;
    [SerializeField] private Player p2;
    private void Update()
    {
        p1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        p2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();


        if (!p1.IsDead) p1.transform.forward = (p2.transform.position - p1.transform.position).normalized;
        if (!p2.IsDead) p2.transform.forward = (p1.transform.position - p2.transform.position).normalized;

        if (Vector3.Distance(p1.transform.position, p2.transform.position) < m_playerMinDist)
        {
            p1.RB.AddForce(-m_playerRepelSpeed * p1.transform.forward);
            p2.RB.AddForce(-m_playerRepelSpeed * p2.transform.forward);
        }

        transform.SetPositionAndRotation(Vector3.Lerp(p1.transform.position, p2.transform.position, 0.5f), Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(p1.transform.forward, Vector3.up), m_targRotSpeed * Time.deltaTime));
        //m_target.SetPositionAndRotation(Vector3.Lerp(p1.position, p2.position, 0.5f), Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(p1.forward, Vector3.up), m_targRotSpeed * Time.deltaTime));
        //transform.SetPositionAndRotation(Vector3.Lerp(transform.position, m_target.position, m_camMoveSpeed * Time.deltaTime), Quaternion.Slerp(transform.rotation, m_target.rotation, m_camRotSpeed * Time.deltaTime));
    }
}
