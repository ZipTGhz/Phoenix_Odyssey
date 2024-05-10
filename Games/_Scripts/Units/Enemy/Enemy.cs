using UnityEngine;

//MANAGER ALL SCRIPTS OF ENEMY
public class Enemy : MonoBehaviour
{
	//SYSTEM
	[SerializeField]
	GameObject statusObjectUI;
	public GameObject StatusObjectUI
	{
		get => statusObjectUI;
	}

	[SerializeField]
	EnemyAI ai;

	[SerializeField]
	EnemyStatus enemyStatus;

	[SerializeField]
	LifeTimeDisabler lifeTimeDisabler;

	public void SetActive(Vector3 position, Quaternion rotation)
	{
		gameObject.SetActive(true);
		statusObjectUI.SetActive(true);
		ai.enabled = true;
		GetComponent<Collider2D>().enabled = true;
		enemyStatus.enabled = true;
		enemyStatus.SetDefaultInfo();
		transform.position = position;
		transform.rotation = rotation;
	}
}
