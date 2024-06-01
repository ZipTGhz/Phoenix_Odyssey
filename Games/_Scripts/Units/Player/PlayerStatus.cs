using System;
using UnityEngine;

public class PlayerStatus : EntityStatus
{
    [Header("STATUS SLIDER & STATUS INFO")]
    [SerializeField]
    StatusBarSlider _manaBarSlider;

    [SerializeField]
    StatusBarSlider _expBarSlider;

    [SerializeField]
    int _maxMana = 10;
    int _currentMana;
    public int CurrentMana
    {
        get => _currentMana;
    }

    protected override void LoadComponents()
    {
        _animator = GetComponentInChildren<Animator>();
        _healthBarSlider = GameObject.FindWithTag("HealthBarOverlay").GetComponent<StatusBarSlider>();
        _manaBarSlider = GameObject.FindWithTag("ManaBarOverlay").GetComponent<StatusBarSlider>();
    }

    protected override void LoadDefaultValue()
    {
        base.LoadDefaultValue();
        _maxHealth = 999;
        _maxMana = 999;
    }

    protected override void Start()
    {
        base.Start();

        _currentMana = _maxMana;
        _manaBarSlider.SetMaxValue(_maxMana);
    }

    protected override void Hurt()
    {
        AnimatorStateInfo stateInfo = GetAnimator().GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Player_Hurt_1"))
            return;
        base.Hurt();
    }

    public void UseMana(int mana)
    {
        _currentMana -= mana;
        _manaBarSlider.SetNewValue(_currentMana);
    }

    public void gainExp(int exp) { }

    protected override void Die()
    {
        // base.Die();

        //Disable player
        //Destroy player
        Destroy(gameObject);
        GameManager.Instance.ChangeState(GameState.GameOver);
    }
}
