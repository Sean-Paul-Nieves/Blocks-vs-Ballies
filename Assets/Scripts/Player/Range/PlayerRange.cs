using UnityEngine;

[RequireComponent(typeof(EnemyRangeDetector))]
[RequireComponent(typeof(RangeVisualizer))]
[RequireComponent(typeof(EnemyRangeDebugLogger))]
public class PlayerRange : MonoBehaviour
{
    public Transform currentTarget { get; private set; }

    [SerializeField] float range = 10f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Color rangeColor = Color.yellow;
    [SerializeField] int rangeSegments = 64;
    [SerializeField] float targetRefreshInterval = 0.05f;

    private EnemyRangeDetector _rangeDetector;
    private RangeVisualizer _rangeVisualizer;
    private readonly RangeTargetSelector _targetSelector = new RangeTargetSelector();
    private float _nextTargetRefreshTime;

    /// <summary>Gets the focused components and applies this object's range settings.</summary>
    private void Awake()
    {
        _rangeDetector = GetComponent<EnemyRangeDetector>();
        _rangeVisualizer = GetComponent<RangeVisualizer>();

        _rangeDetector.Configure(range, enemyLayer);
        _rangeVisualizer.Configure(range, rangeColor, rangeSegments);
    }

    /// <summary>Detects enemies and delegates selection of the closest target.</summary>
    private void Update()
    {
        if (Time.time < _nextTargetRefreshTime)
        {
            return;
        }

        _nextTargetRefreshTime = Time.time + Mathf.Max(0f, targetRefreshInterval);
        int enemyCount = _rangeDetector.Detect(transform);
        currentTarget = _targetSelector.SelectClosest(transform, _rangeDetector, enemyCount);
    }
}
