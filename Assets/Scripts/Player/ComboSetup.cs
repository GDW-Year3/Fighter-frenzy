using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AttackType { Y = 0, X = 1, A = 2, B = 3 };
public class ComboSetup : MonoBehaviour
{
    [Header("Attacks")]
    public Attack Y_Attack;
    public Attack X_Attack;
    public Attack A_Attack;
    public Attack B_Attack;
    public List<Combo> m_combos;
    public float m_comboLeeway = 0.2f;

    [Header("Components")]
    private Animator m_animator;
    public UserInput m_userInput;

    Attack m_curAttack = null;
    ComboInput m_lastInput = null;
    List<int> m_currentCombos = new();
    float m_timer = 0f;
    float m_leeway = 0f;
    bool m_skip = false;

    private Player m_thisPlayer;
    void Start()
    {
        m_thisPlayer = GetComponent<Player>();
        m_animator = GetComponentInChildren<Animator>();
        PrimeCombos();
    }
    void PrimeCombos()
    {
        for (int i = 0; i < m_combos.Count; i++)
        {
            Combo c = m_combos[i];
            c.m_onInputted.AddListener(() =>
            {
                m_skip = true;
                Attack(c.m_comboAttack);
                ResetCombos();
            });
        }
    }
    void Update()
    {
        if (m_currentCombos.Count > 0)
        {
            m_leeway += Time.deltaTime;
            if (m_leeway >= m_comboLeeway)
            {
                if (m_lastInput != null)
                {
                    Attack(GetAttackFromType(m_lastInput.m_type));
                    m_lastInput = null;
                }
                ResetCombos();
            }
        }
        else m_leeway = 0f;

        ComboInput input = null;
        if (m_userInput.Y) input = new ComboInput(AttackType.Y);
        if (m_userInput.X) input = new ComboInput(AttackType.X);
        if (m_userInput.A) input = new ComboInput(AttackType.A);
        if (m_userInput.B) input = new ComboInput(AttackType.B);
        if (input == null) return;
        m_lastInput = input;

        List<int> remove = new();
        for (int i = 0; i < m_currentCombos.Count; i++)
        {
            if (m_combos[m_currentCombos[i]].ContinueCombo(input)) m_leeway = 0f;
            else remove.Add(i);
        }
        if (m_skip)
        {
            m_skip = false;
            return;
        }
        for (int i = 0; i < m_combos.Count; i++)
        {
            if (m_currentCombos.Contains(i)) continue;
            if (m_combos[i].ContinueCombo(input))
            {
                m_currentCombos.Add(i);
                m_leeway = 0f;
            }
        }
        for (int i = remove.Count - 1; i >= 0; i--)
        {
            m_currentCombos.RemoveAt(remove[i]);
        }

        if (m_currentCombos.Count <= 0) Attack(GetAttackFromType(input.m_type));
    }
    void ResetCombos()
    {
        m_leeway = 0f;
        for (int i = 0; i < m_currentCombos.Count; i++)
        {
            m_combos[m_currentCombos[i]].ResetCombo();
        }
        m_currentCombos.Clear();
    }
    void Attack(Attack att)
    {
        if (att.m_attackingPartsList.Count > 0)
        {
            if (!att.m_running)
            {
                StartCoroutine(RunAttack(att));
            }
            ///
            /// use a dictionanry with the key(s) as the start time of the attackingPart's hit
            /// simply grab the DMGDealer associated with the current time stamp and enable it
            /// use a second dcitionary with the same storage method only now use the end time of the specific attack
            /// should be able to enable and disable the same DMGDealer multiple times in a single attack animation
            ///
        }
        else Debug.LogWarning("No attacking part(s) associated with " + att.m_clip.name + " on " + gameObject.name);
    }
    Attack GetAttackFromType(AttackType t)
    {
        if (t == AttackType.Y) return Y_Attack;
        if (t == AttackType.X) return X_Attack;
        if (t == AttackType.A) return A_Attack;
        if (t == AttackType.B) return B_Attack;
        return null;
    }
    private IEnumerator RunAttack(Attack att)
    {
        m_thisPlayer.Source.clip = m_thisPlayer.SFXAtk[(int)Random.Range(0f, m_thisPlayer.SFXAtk.Length - 1f)];
        m_thisPlayer.Source.Play();
        att.m_running = true;
        m_animator.SetInteger("attack", att.m_index);
        foreach (DMGDealer d in att.m_attackingPartsList)
        {
            d.CanAttack = true;
            d.Damage = att.m_totalDamage / att.m_numHits;
        }
        yield return new WaitForSeconds(att.Length);
        foreach (DMGDealer d in att.m_attackingPartsList)
        {
            d.CanAttack = false;
            d.Hit = false;
            d.Damage = 0f;
        }
        att.m_running = false;
        m_animator.SetInteger("attack", 0);
    }
}
[System.Serializable]
public class Attack
{
    public AnimationClip m_clip;
    public int m_index;
    public float m_speed;
    public float m_totalDamage;
    public int m_numHits = 0;
    public bool m_running = false;
    [SerializeField]
    public List<DMGDealer> m_attackingPartsList = new();
    public float Length => m_clip.length / m_speed;
}
[System.Serializable]
public class ComboInput
{
    public AttackType m_type;
    public ComboInput(AttackType t)
    {
        m_type = t;
    }
    public bool IsSameAs(ComboInput test)
    {
        return (m_type == test.m_type);
    }
}
[System.Serializable]
public class Combo
{
    public List<ComboInput> m_inputs;
    public Attack m_comboAttack;
    public UnityEvent m_onInputted;
    int m_curInput = 0;
    public bool ContinueCombo(ComboInput i)
    {
        if (m_inputs[m_curInput].IsSameAs(i))
        {
            m_curInput++;
            if (m_curInput >= m_inputs.Count)
            {
                m_onInputted.Invoke();
                m_curInput = 0;
            }
            return true;
        }
        else
        {
            m_curInput = 0;
            return false;
        }
    }
    public ComboInput CurrentComboInput()
    {
        if (m_curInput >= m_inputs.Count) return null;
        return m_inputs[m_curInput];
    }
    public void ResetCombo()
    {
        m_curInput = 0;
    }
}