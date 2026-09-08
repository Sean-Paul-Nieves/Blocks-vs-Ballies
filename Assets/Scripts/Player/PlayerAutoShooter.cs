using UnityEngine;

[RequireComponent(typeof(PlayerRange))]
[RequireComponent(typeof(VisualProjectilePool))]
public class PlayerAutoShooter : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float shotsPerSecond = 2f;
    [SerializeField] float projectileSpeed = 25f;
    [SerializeField] float projectileSize = 0.15f;
    [SerializeField] Color projectileColor = Color.cyan;
    [SerializeField] bool logSuccessfulShots;

    private PlayerRange _playerRange;
    private VisualProjectilePool _projectilePool;
    private float _nextShotTime;

    /// <summary>Gets the range target provider used by the auto-shooter.</summary>
    private void Awake()
    {
        _playerRange = GetComponent<PlayerRange>();
        _projectilePool = GetComponent<VisualProjectilePool>();
    }

    /// <summary>Fires at the current live target whenever the fire cooldown allows it.</summary>
    private void Update()
    {
        if (Time.time < _nextShotTime || _playerRange.currentTarget == null)
        {
            return;
        }

        if (TryShootCurrentTarget())
        {
            _nextShotTime = Time.time + 1f / Mathf.Max(0.01f, shotsPerSecond);
        }
    }

    /// <summary>Applies damage directly to the target's current damageable component.</summary>
    private bool TryShootCurrentTarget()
    {
        Transform target = _playerRange.currentTarget;
        IDamageable damageable = target.GetComponentInParent<IDamageable>();

        if (damageable == null)
        {
            Debug.LogWarning($"Target {target.name} does not implement IDamageable.", target);
            return false;
        }

        damageable.TakeDamage(damage);
        _projectilePool.TryLaunch(
            transform.position,
            target,
            projectileSpeed,
            projectileSize,
            projectileColor);
        if (logSuccessfulShots)
        {
            Debug.Log($"Auto-shot hit: {target.name} for {damage} damage.", target);
        }
        return true;
    }
}
