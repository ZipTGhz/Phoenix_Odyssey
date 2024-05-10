using UnityEngine;

public abstract class EntityStatus : MonoBehaviour
{
    //SYSTEM
    private Animator animator;

    //STATUS SLIDER & STATUS INFO
    public StatusBarSlider healthBarSlider;
    public float maxHealth = 100f;
    float currentHealth;

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        SetDefaultInfo();
    }

    public void SetDefaultInfo()
    {
        currentHealth = maxHealth;
        healthBarSlider.SetMaxValue(maxHealth);
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public void TakeDamage(float damage)
    {
        //Set new hp value
        currentHealth -= damage;
        healthBarSlider.SetNewValue(currentHealth);

        // Play hurt animation
        Hurt();
        //Play die animation
        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Hurt()
    {
        animator.SetTrigger("TakeHit");
    }

    protected virtual void Die()
    {
        //Play die animation
        animator.SetBool("IsDeath", true);
    }
}
