using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class EnemySpawner2D : MonoBehaviour
{
    //SYSTEM
    ObjectPooler objectPooler;

    //SPAWNER INFO
    public float timeStart = 0;
    public float repeatRate = 5;

    void Start()
    {
        objectPooler = GetComponent<ObjectPooler>();
        InvokeRepeating("Spawn", timeStart, repeatRate);
    }

    void Spawn()
    {
        GameObject enemy = objectPooler.GetPooledObject();
        if (enemy == null)
            return;
        enemy.GetComponent<Enemy>().SetActive(transform.position, Quaternion.identity);
    }
}
