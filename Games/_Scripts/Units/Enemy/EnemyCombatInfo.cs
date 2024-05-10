using UnityEngine;

public class EnemyCombatInfo : MonoBehaviour
{
	//SYSTEM
	public Transform attackPoint;
	public LayerMask playerMask;

	//ATTACK INFO
	public float flameDamage = 1f;
	public Vector3 attackRange = new Vector3(1, 0.6f, 0);

	public void Attack()
	{
		Collider2D hitInfo = Physics2D.OverlapBox(attackPoint.position, attackRange, 0, playerMask);
		if (hitInfo != null)
			hitInfo.GetComponent<PlayerStatus>().TakeDamage(flameDamage);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(attackPoint.position, attackRange);
	}
}
