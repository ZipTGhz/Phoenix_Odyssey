using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public int maxEnemyCount = 10;
    int currentEnemyCount = 0;

    void Start()
    {
        InvokeRepeating("Spawn", 0f, 5f);
    }

    void Spawn()
    {
        if (currentEnemyCount >= maxEnemyCount)
            return;
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        ++currentEnemyCount;
    }

    public void EnemyKilled()
    {
        --currentEnemyCount;
    }
}
