using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] private float m_maxHp;
    [SerializeField] private float m_baseAttack;
    [SerializeField] private float m_blockingFactor;
    [SerializeField] private float m_comboMulti;
    [SerializeField] private float m_chainMulti;
    [Header("Movement")]
    [SerializeField] private float m_walkSpeed;
    [SerializeField] private float m_strafeSpeed;
    [Header("Prefabs")]
    [SerializeField] private UIAnimation[] HPBarPrefabs;
    [Header("SFX")]
    [SerializeField] private AudioClip[] HitSFX;
    [SerializeField] private AudioClip[] StepsSFX;
    [SerializeField] private AudioClip[] AttackSFX;
    [Header("Animations")]
    [SerializeField] private AnimationClip[] deathClips;
    [SerializeField] private AnimationClip[] hitClips;
    [Header("Colors")]
    [SerializeField] private List<Material> m_materials = new List<Material>();

    private bool m_isGrounded = true;
    private bool m_isAttacking = false;
    private bool m_isDead = false;

    private int m_currentChain = 0;

    private float m_chainTimer = 0f;
    private float m_currentHp;
    private float m_currentDMG;

    private Animator m_animator;
    private AudioSource m_audioSource;
    private Rigidbody m_rb;

    private UIAnimation HPBar;
    private UserInput m_userInput;
    private DMGDealer[] dMGDealers;
    void Start()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_userInput = GetComponent<UserInput>();
        m_rb = GetComponent<Rigidbody>();
        m_audioSource = GetComponent<AudioSource>();
        m_currentHp = m_maxHp;
        m_currentDMG = m_baseAttack;
        HPBar = Instantiate(HPBarPrefabs[m_userInput.ID], FindObjectOfType<Canvas>().transform);
        dMGDealers = GetComponentsInChildren<DMGDealer>();
    }
    void Update()
    {
        if (m_isDead) return;
        foreach(DMGDealer d in dMGDealers)
        {
            if (d.Hit) { m_currentChain++; m_chainTimer = 0f; }
        }
        if (m_currentChain > 0) m_chainTimer += Time.deltaTime;
        if (m_chainTimer > 2f) m_currentChain = 0;
        HPBar.SliderBar(m_maxHp, m_currentHp);

        if (m_comboMulti < 1f) m_comboMulti = 1f;
        if (m_chainMulti < 1f) m_chainMulti = 1f;
        m_currentDMG = m_baseAttack * m_comboMulti * m_chainMulti;

        m_isGrounded = CheckGround();

        if (m_userInput.Vertin != 0f || m_userInput.Horzin != 0f)
        {
            Vector3 vel = (m_walkSpeed * m_userInput.Vertin * transform.forward + m_strafeSpeed * m_userInput.Horzin * transform.right);
            m_rb.velocity = new(vel.x, m_rb.velocity.y > 0f ? 0f : m_rb.velocity.y, vel.z);
        }
        else m_rb.velocity = new(0f, m_rb.velocity.y > 0f ? 0f : m_rb.velocity.y, 0F);

        m_animator.SetFloat("vert", m_userInput.Vertin);
        m_animator.SetFloat("horz", m_userInput.Horzin);
    }
    private bool CheckGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.125f);
    }
    public void TakeDamage(bool blockable, float d)
    {
        if (m_isDead) return;
        m_audioSource.clip = HitSFX[(int) Random.Range(0f, HitSFX.Length - 1f)];
        m_audioSource.Play();
        int i = (int)Random.Range(0f, hitClips.Length - 1f);
        if (hitClips.Length > 0) m_animator.SetInteger("hit", i + 1);
        StartCoroutine(DelayResetInt("hit", i));
        if (m_userInput.Blocking && blockable) d *= m_blockingFactor;
        m_currentHp -= d;
        if (m_currentHp <= 0) 
        { 
            m_currentHp = 0f;
            StartCoroutine(Die()); 
        }
    }
    private IEnumerator Die()
    {
        int i = (int)Random.Range(0f, deathClips.Length - 1f);
        AnimationClip c = deathClips[i];
        m_isDead = true;
        HPBar.SliderBar(m_maxHp, m_currentHp);
        m_animator.SetInteger("die", i + 1);
        yield return new WaitForSeconds(c.length);
        m_animator.SetInteger("die", 0);
        m_animator.enabled = false;
    }
    private IEnumerator DelayResetInt(string s, float t)
    {
        yield return new WaitForSeconds(t);
        m_animator.SetInteger(s, 0);
    }
    public bool IsDead => m_isDead;
    public float CurrentDMG => m_currentDMG;
    public bool IsAttacking
    {
        get { return m_isAttacking; }
        set { m_isAttacking = value; }
    }
    public Rigidbody RB => m_rb;
    public AudioSource Source => m_audioSource;
    public AudioClip[] SFXAtk => AttackSFX;
    public AudioClip[] SFXsteps => StepsSFX;
    public List<Material> Materials => m_materials;
}