using UnityEngine;

public abstract class EntityStatus : CustomMonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField]
    protected Animator _animator;

    [Header("STATUS SLIDER & STATUS INFO")]
    [SerializeField]
    protected StatusBarSlider _healthBarSlider;

    [SerializeField]
    protected float _maxHealth = 100f;

    protected float _currentHealth;
    public float CurrentHealth
    {
        get => _currentHealth;
    }



    protected virtual void Start()
    {
        SetDefaultInfo();
    }

    public void SetDefaultInfo()
    {
        _currentHealth = _maxHealth;
        _healthBarSlider.SetMaxValue(_maxHealth);
    }

    public Animator GetAnimator()
    {
        return _animator;
    }

    public void TakeDamage(float damage)
    {
        //Set new hp value
        _currentHealth -= damage;
        _healthBarSlider.SetNewValue(_currentHealth);

        // Play hurt animation
        Hurt();
        //Play die animation
        if (_currentHealth <= 0)
            Die();
    }

    protected virtual void Hurt()
    {
        _animator.SetTrigger("TakeHit");
    }

    protected virtual void Die()
    {
        //Play die animation
        _animator.SetBool("IsDeath", true);
    }
}
