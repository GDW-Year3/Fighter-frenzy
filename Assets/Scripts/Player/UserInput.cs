using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    [SerializeField] private int m_id;

    private bool x, y, a, b, m_jump, m_blocking;

    private float m_vertin, m_horzin;
    private float m_Dvertin = 0f, m_Dhorzin = 0f;
    void Update()
    {
        m_vertin = Mathf.Lerp(m_vertin, Input.GetAxisRaw("Xbox_LeftStickX" + m_id), 4f * Time.deltaTime);
        m_horzin = Mathf.Lerp(m_horzin, Input.GetAxisRaw("Xbox_LeftStickY" + m_id), 4f * Time.deltaTime);
        Mathf.Clamp(m_vertin, -1f, 1f);
        Mathf.Clamp(m_horzin, -1f, 1f);

        m_Dvertin = Mathf.Lerp(m_Dvertin, Input.GetAxisRaw("Xbox_DpadV" + m_id), 4f * Time.deltaTime);
        m_Dhorzin = Mathf.Lerp(m_Dhorzin, Input.GetAxisRaw("Xbox_DpadH" + m_id), 4f * Time.deltaTime);
        Mathf.Clamp(m_Dvertin, -1f, 1f);
        Mathf.Clamp(m_Dhorzin, -1f, 1f);

        a = Input.GetButtonDown("Xbox_A" + m_id);
        b = Input.GetButtonDown("Xbox_B" + m_id);
        x = Input.GetButtonDown("Xbox_X" + m_id);
        y = Input.GetButtonDown("Xbox_Y" + m_id);

        m_jump = Input.GetKey(KeyCode.Space);
        m_blocking = Input.GetKey(KeyCode.Q);
    }
    public bool X => x;
    public bool Y => y;
    public bool A => a;
    public bool B => b;
    public bool Jump => m_jump;
    public bool Blocking => m_blocking;
    public int ID => m_id;
    public float Vertin => Mathf.Abs(m_vertin) > Mathf.Abs(m_Dvertin) ? m_vertin : m_Dvertin;
    public float Horzin => Mathf.Abs(m_horzin) > Mathf.Abs(m_Dhorzin) ? m_horzin : m_Dhorzin;

    public void setMyID(int newID)
    {
        m_id = newID;
    }

}
