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

    [SerializeField] private UIAnimation[] HPBarPrefabs;
    [SerializeField] private AudioClip[] HitSFX;
    [SerializeField] private AudioClip[] StepsSFX;
    [SerializeField] private AudioClip[] AttackSFX;

    [SerializeField] private AnimationClip[] deathClips;
    [SerializeField] private AnimationClip[] hitClips;

    private bool m_isGrounded = true;
    private bool m_isAttacking = false;

    private int m_currentChain = 0;

    private float m_currentHp;
    private float m_currentDMG;

    private Animator m_animator;
    private AudioSource m_audioSource;

    private UIAnimation HPBar;

    private UserInput m_userInput;
    void Start()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_userInput = GetComponent<UserInput>();
        m_audioSource = FindObjectOfType<CameraCTRL>().GetComponent<AudioSource>();
        m_currentHp = m_maxHp;
        m_currentDMG = m_baseAttack;
        HPBar = Instantiate(HPBarPrefabs[m_userInput.ID], FindObjectOfType<Canvas>().transform);
    }
    void Update()
    {
        HPBar.SliderBar(m_maxHp, m_currentHp);

        if (m_comboMulti < 1f) m_comboMulti = 1f;
        if (m_chainMulti < 1f) m_chainMulti = 1f;
        m_currentDMG = m_baseAttack * m_comboMulti * m_chainMulti;

        m_isGrounded = CheckGround();

        m_animator.SetFloat("vert", m_userInput.Vertin);
        m_animator.SetFloat("horz", m_userInput.Horzin);
    }
    private bool CheckGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.125f);
    }
    public void TakeDamage(bool blockable, float d)
    {
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
        m_animator.SetInteger("die", i + 1);
        yield return new WaitForSeconds(c.length);

        m_animator.SetInteger("die", 0);
        m_animator.enabled = false;
        this.enabled = false;
    }
    private IEnumerator DelayResetInt(string s, float t)
    {
        yield return new WaitForSeconds(t);
        m_animator.SetInteger(s, 0);
    }
    public float CurrentDMG
    {
        get { return m_currentDMG; }
    }
    public bool IsAttacking
    {
        get { return m_isAttacking; }
        set { m_isAttacking = value; }
    }
    public AudioSource Source
    {
        get { return m_audioSource; }
    }
    public AudioClip[] SFXAtk
    {
        get { return AttackSFX; }
    }
    public AudioClip[] SFXsteps
    {
        get { return StepsSFX; }
    }
}