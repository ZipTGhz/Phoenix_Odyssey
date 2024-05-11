using UnityEngine;

public abstract class EntityStatus : MonoBehaviour
{
    //SYSTEM
    Animator _animator;

    //STATUS SLIDER & STATUS INFO
    public StatusBarSlider healthBarSlider;
    public float maxHealth = 100f;
    float _currentHealth;
    public float CurrentHealth
    {
        get => _currentHealth;
    }

    protected virtual void Awake()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        SetDefaultInfo();
    }

    public void SetDefaultInfo()
    {
        _currentHealth = maxHealth;
        healthBarSlider.SetMaxValue(maxHealth);
    }

    public Animator GetAnimator()
    {
        return _animator;
    }

    public void TakeDamage(float damage)
    {
        //Set new hp value
        _currentHealth -= damage;
        healthBarSlider.SetNewValue(_currentHealth);

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
