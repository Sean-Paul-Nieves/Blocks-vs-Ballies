using UnityEngine;

public class EnemyRangeDebugLogger : MonoBehaviour
{
    private EnemyRangeDetector _rangeDetector;

    /// <summary>Subscribes to enemy-entry events from the detector on the same GameObject.</summary>
    private void OnEnable()
    {
        _rangeDetector = GetComponent<EnemyRangeDetector>();
        _rangeDetector.EnemyEnteredRange += LogEnemyEnteredRange;
    }

    /// <summary>Stops listening when this logger is disabled or destroyed.</summary>
    private void OnDisable()
    {
        if (_rangeDetector != null)
        {
            _rangeDetector.EnemyEnteredRange -= LogEnemyEnteredRange;
        }
    }

    /// <summary>Writes the entering enemy to the Unity Console and highlights its object.</summary>
    private void LogEnemyEnteredRange(Collider enemy)
    {
        Debug.Log($"Enemy entered range: {enemy.name}", enemy);
    }
}
