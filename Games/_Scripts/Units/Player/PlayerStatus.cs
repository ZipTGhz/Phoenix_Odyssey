using System;
using UnityEngine;

public class PlayerStatus : EntityStatus
{
    //STATUS SLIDER & STATUS INFO
    public StatusBarSlider ManaBarSlider;
    public StatusBarSlider ExpBarSlider;

    [SerializeField]
    int _maxMana = 10;
    int _currentMana;
    public int CurrentMana
    {
        get => _currentMana;
    }

    protected override void Start()
    {
        base.Start();

        _currentMana = _maxMana;
        ManaBarSlider.SetMaxValue(_maxMana);
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
        ManaBarSlider.SetNewValue(_currentMana);
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
