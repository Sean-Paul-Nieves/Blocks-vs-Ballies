using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawnPooling : MonoBehaviour
{
    IObjectPool<EnemySpawnPooling> enemyPool;

    public void SetPool (IObjectPool<EnemySpawnPooling> pool)
    {
        enemyPool = pool;
    }
}
