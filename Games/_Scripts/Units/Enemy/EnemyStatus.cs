using UnityEngine;

public class EnemyStatus : EntityStatus
{
    //SYSTEM
    EnemyController enemyController;

    protected override void LoadComponents()
    {
        _animator = GetComponentInChildren<Animator>();
        _healthBarSlider = GetComponentInChildren<StatusBarSlider>();
    }

    protected override void Start()
    {
        base.Start();
        enemyController = GetComponent<EnemyController>();
    }

    protected override void Die()
    {
        base.Die();

        // Disable the enemy and hide object
        enemyController.SetDeactivate();
    }
}
