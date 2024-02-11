using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCTRL : MonoBehaviour
{
    [SerializeField] private float m_camRotSpeed;
    [SerializeField] private float m_playerRotSpeed;
    [SerializeField] private float m_playerMinDist;
    [SerializeField] private Transform p1;
    [SerializeField] private Transform p2;

    private void Update()
    {
        p1.rotation = Quaternion.Slerp(p1.rotation, Quaternion.LookRotation((p2.position - p1.position).normalized, Vector3.up), m_playerRotSpeed * Time.deltaTime);
        p2.rotation = Quaternion.Slerp(p2.rotation, Quaternion.LookRotation((p1.position - p2.position).normalized, Vector3.up), m_playerRotSpeed * Time.deltaTime);

        if (Vector3.Distance(p1.position, p2.position) < m_playerMinDist)
        {
            p1.position = (transform.position - p1.position).normalized * (m_playerMinDist * -0.5f);
            p2.position = (transform.position - p2.position).normalized * (m_playerMinDist * -0.5f);
        }

        transform.SetPositionAndRotation(Vector3.Lerp(p1.position, p2.position, 0.5f), Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(p1.forward, Vector3.up), m_camRotSpeed * Time.deltaTime));
    }
}
