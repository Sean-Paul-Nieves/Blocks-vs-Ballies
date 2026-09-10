using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float timeBetweenSpawns = 2f;
    [SerializeField] private GameObject enemyPrefab;

    private float timeSinceLastSpawn;

    private IObjectPool<EnemySpawnPooling> enemyPool;

    void Awake()
    {
        enemyPool = new ObjectPool<EnemySpawnPooling>(
            CreateEnemy,
            OnGet,
            OnRelease
        );
    }

    private EnemySpawnPooling CreateEnemy()
    {
        EnemySpawnPooling enemy =
            Instantiate(enemyPrefab).GetComponent<EnemySpawnPooling>();

        enemy.SetPool(enemyPool);

        return enemy;
    }

    private void OnGet(EnemySpawnPooling enemy)
    {
        Transform spawnPoint =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        enemy.transform.position = spawnPoint.position;
        enemy.transform.rotation = spawnPoint.rotation;

        enemy.gameObject.SetActive(true);
    }

    private void OnRelease(EnemySpawnPooling enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Time.time >= timeSinceLastSpawn)
        {
            enemyPool.Get();

            timeSinceLastSpawn = Time.time + timeBetweenSpawns;
        }
    }
}