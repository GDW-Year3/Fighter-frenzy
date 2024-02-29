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
    private Transform m_target;
    private void Awake()
    {
        m_target = Instantiate(new GameObject()).transform;
    }
    private void Update()
    {
        //p1.rotation = Quaternion.Slerp(p1.rotation, Quaternion.LookRotation((p2.position - p1.position).normalized, Vector3.up), m_playerRotSpeed * Time.deltaTime);
        //p2.rotation = Quaternion.Slerp(p2.rotation, Quaternion.LookRotation((p1.position - p2.position).normalized, Vector3.up), m_playerRotSpeed * Time.deltaTime);
        if (!p1.IsDead) p1.transform.forward = (p2.transform.position - p1.transform.position).normalized;
        if (!p2.IsDead) p2.transform.forward = (p1.transform.position - p2.transform.position).normalized;

        if (Vector3.Distance(p1.transform.position, p2.transform.position) < m_playerMinDist)
        {
            /*Vector3 pos = (transform.position - p1.transform.position).normalized * (m_playerMinDist * -0.5f);
            p1.transform.position = new Vector3(pos.x, p1.transform.position.y, pos.z);
            pos = (transform.position - p2.transform.position).normalized * (m_playerMinDist * -0.5f);
            p2.transform.position = new Vector3(pos.x, p2.transform.position.y, pos.z);*/
            p1.RB.AddForce(-m_playerRepelSpeed * p1.transform.forward);
            p2.RB.AddForce(-m_playerRepelSpeed * p2.transform.forward);
            //p1.transform.position = Vector3.Lerp(p1.transform.position, 0.5f * m_playerMinDist * (p2.transform.position - p1.transform.position).normalized, m_playerRepelSpeed * Time.deltaTime);
            //p2.transform.position = Vector3.Lerp(p2.transform.position, 0.5f * m_playerMinDist * (p1.transform.position - p2.transform.position).normalized, m_playerRepelSpeed * Time.deltaTime);
        }

        transform.SetPositionAndRotation(Vector3.Lerp(p1.transform.position, p2.transform.position, 0.5f), Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(p1.transform.forward, Vector3.up), m_targRotSpeed * Time.deltaTime));
        //m_target.SetPositionAndRotation(Vector3.Lerp(p1.position, p2.position, 0.5f), Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(p1.forward, Vector3.up), m_targRotSpeed * Time.deltaTime));
        //transform.SetPositionAndRotation(Vector3.Lerp(transform.position, m_target.position, m_camMoveSpeed * Time.deltaTime), Quaternion.Slerp(transform.rotation, m_target.rotation, m_camRotSpeed * Time.deltaTime));
    }
}
