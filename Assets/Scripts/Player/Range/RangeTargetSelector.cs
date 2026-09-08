using UnityEngine;

public sealed class RangeTargetSelector
{
    /// <summary>Returns the closest enemy to the origin, or null when no enemies are detected.</summary>
    public Transform SelectClosest(Transform origin, EnemyRangeDetector detector, int enemyCount)
    {
        float closestDistanceSqr = Mathf.Infinity;
        Transform closest = null;

        for (int i = 0; i < enemyCount; i++)
        {
            Collider enemy = detector.GetDetectedCollider(i);
            if (enemy == null)
            {
                continue;
            }

            float distanceSqr = (origin.position - enemy.transform.position).sqrMagnitude;

            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closest = enemy.transform;
            }
        }

        return closest;
    }
}
