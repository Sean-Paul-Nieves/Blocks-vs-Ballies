using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeDetector : MonoBehaviour
{
    [SerializeField] float _range = 10f;
    [SerializeField] LayerMask _enemyLayer;

    [SerializeField] private int initialBufferSize = 32;

    private readonly HashSet<Collider> _previousEnemiesInRange = new HashSet<Collider>();
    private readonly HashSet<Collider> _currentEnemiesInRange = new HashSet<Collider>();
    private Collider[] _colliderBuffer;
    private int _detectedCount;

    public float Range => _range;

    public event Action<Collider> EnemyEnteredRange;

    /// <summary>Allocates the reusable physics-query buffer once for this detector.</summary>
    private void Awake()
    {
        _colliderBuffer = new Collider[Mathf.Max(1, initialBufferSize)];
    }

    /// <summary>Updates the physics query settings owned by this detector.</summary>
    public void Configure(float range, LayerMask enemyLayer)
    {
        _range = range;
        _enemyLayer = enemyLayer;
    }

    /// <summary>Refreshes the reusable overlap buffer and raises events for newly detected enemies.</summary>
    public int Detect(Transform origin)
    {
        _detectedCount = Physics.OverlapSphereNonAlloc(
            origin.position, _range, _colliderBuffer, _enemyLayer);

        while (_detectedCount == _colliderBuffer.Length)
        {
            System.Array.Resize(ref _colliderBuffer, _colliderBuffer.Length * 2);
            _detectedCount = Physics.OverlapSphereNonAlloc(
                origin.position, _range, _colliderBuffer, _enemyLayer);
        }

        _currentEnemiesInRange.Clear();

        for (int i = 0; i < _detectedCount; i++)
        {
            Collider enemy = _colliderBuffer[i];
            if (enemy == null)
            {
                continue;
            }

            _currentEnemiesInRange.Add(enemy);

            if (!_previousEnemiesInRange.Contains(enemy))
            {
                EnemyEnteredRange?.Invoke(enemy);
            }
        }

        _previousEnemiesInRange.Clear();
        _previousEnemiesInRange.UnionWith(_currentEnemiesInRange);
        return _detectedCount;
    }

    /// <summary>Returns a detected collider from the reusable query buffer.</summary>
    public Collider GetDetectedCollider(int index)
    {
        return _colliderBuffer[index];
    }
}
