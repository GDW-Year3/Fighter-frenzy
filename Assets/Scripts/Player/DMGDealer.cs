using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGDealer : MonoBehaviour
{
    [SerializeField] private bool m_canBeBlocked = true;

    private Player m_player;

    private bool m_canAttack = false;
    private bool m_hit = false;
    private float m_damage;
    private void Start()
    {
        m_player = GetComponentInParent<Player>();
    }
    private void UseAttack(Player p)
    {
        m_player.Source.clip = m_player.SFXAtk[(int)Random.Range(0f, m_player.SFXAtk.Length - 1f)];
        m_player.Source.Play();
        Debug.Log(m_player.Source.clip);
        p.TakeDamage(m_canBeBlocked, m_damage);
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.layer == 6 || other.gameObject.layer == 7) && m_canAttack && !m_hit)
        {
            UseAttack(other.gameObject.GetComponentInParent<Player>());
            m_hit = true;
            Debug.Log("Hit" + other.name);
        }
    }
    public bool CanAttack
    {
        get { return m_canAttack; }
        set { m_canAttack = value; }
    }
    public bool Hit
    {
        get { return m_hit; }
        set { m_hit = value; }
    }
    public float Damage
    {
        set { m_damage = value; }
    }
}
