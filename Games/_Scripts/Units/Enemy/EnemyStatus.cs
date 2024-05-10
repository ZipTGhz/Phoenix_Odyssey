using UnityEngine;

public class EnemyStatus : EntityStatus
{
	//SYSTEM
	Enemy enemy;
	EnemyAI ai;

	protected override void Start()
	{
		base.Start();
		enemy = GetComponent<Enemy>();
		ai = GetComponent<EnemyAI>();
	}

	protected override void Die()
	{
		base.Die();

		// Disable the enemy and hide object
		Disable();
	}

	void Disable()
	{
		enemy.StatusObjectUI.SetActive(false);
		ai.enabled = false;
		GetComponent<Collider2D>().enabled = false;
		this.enabled = false;
		GetComponentInParent<LifeTimeDisabler>().DelayedDisable();
	}
}
