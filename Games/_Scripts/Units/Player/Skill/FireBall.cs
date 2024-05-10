using UnityEngine;

public class FireBall : MonoBehaviour
{
	float speed;
	Rigidbody2D rb;
	float fireBallDamage;

	// Start is called before the first frame update
	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	public void SetActive(Vector3 position, Quaternion rotation, float speed, float fireBallDamage)
	{
		gameObject.SetActive(true);
		GetComponent<LifeTimeDisabler>().DelayedDisable();
		transform.position = position;
		transform.rotation = rotation;
		this.speed = speed;
		this.fireBallDamage = fireBallDamage;
		rb.velocity = transform.right * speed;
	}

	void OnTriggerEnter2D(Collider2D hitInfo)
	{
		EnemyStatus enemy = hitInfo.GetComponentInChildren<EnemyStatus>();
		if (enemy == null)
			return;
		enemy.TakeDamage(fireBallDamage);
		gameObject.SetActive(false);
	}
}
