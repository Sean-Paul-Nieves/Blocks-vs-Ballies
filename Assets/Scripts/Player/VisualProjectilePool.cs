using System.Collections.Generic;
using UnityEngine;

/// <summary>Creates, reuses, and releases cosmetic projectiles without per-shot object churn.</summary>
public sealed class VisualProjectilePool : MonoBehaviour
{
    [SerializeField] private VisualProjectile projectilePrefab;
    [SerializeField] private int initialPoolSize = 16;
    [SerializeField] private int maxPoolSize = 128;
    [SerializeField] private Transform projectileContainer;

    private readonly Stack<VisualProjectile> _availableProjectiles = new Stack<VisualProjectile>();
    private readonly List<VisualProjectile> _activeProjectiles = new List<VisualProjectile>();
    private int _createdProjectiles;

    /// <summary>Prewarms the pool so the first shots do not create objects during combat.</summary>
    private void Awake()
    {
        maxPoolSize = Mathf.Max(1, maxPoolSize);
        initialPoolSize = Mathf.Clamp(initialPoolSize, 0, maxPoolSize);

        if (projectileContainer == null)
        {
            GameObject container = new GameObject("Visual Projectiles");
            projectileContainer = container.transform;
            projectileContainer.SetParent(transform.parent);
        }

        for (int i = 0; i < initialPoolSize; i++)
        {
            _availableProjectiles.Push(CreateProjectile());
        }
    }

    /// <summary>Updates all active visuals from one manager callback instead of one callback per projectile.</summary>
    private void Update()
    {
        for (int i = _activeProjectiles.Count - 1; i >= 0; i--)
        {
            _activeProjectiles[i].Tick(Time.deltaTime);
        }
    }

    /// <summary>Gets a projectile from the pool and launches it toward the target.</summary>
    public bool TryLaunch(
        Vector3 origin,
        Transform target,
        float speed,
        float size,
        Color color)
    {
        VisualProjectile projectile = GetProjectile();
        if (projectile == null)
        {
            return false;
        }

        projectile.transform.SetPositionAndRotation(origin, Quaternion.identity);
        projectile.gameObject.SetActive(true);
        projectile.Launch(target, speed, size, color, Release);
        _activeProjectiles.Add(projectile);
        return true;
    }

    /// <summary>Returns a completed projectile to the inactive pool.</summary>
    private void Release(VisualProjectile projectile)
    {
        _activeProjectiles.Remove(projectile);
        projectile.ResetForPool();
        projectile.gameObject.SetActive(false);
        _availableProjectiles.Push(projectile);
    }

    /// <summary>Gets an available projectile or creates one if the pool has capacity.</summary>
    private VisualProjectile GetProjectile()
    {
        if (_availableProjectiles.Count > 0)
        {
            return _availableProjectiles.Pop();
        }

        return _createdProjectiles < maxPoolSize ? CreateProjectile() : null;
    }

    /// <summary>Creates one reusable projectile from the assigned prefab or a fallback sphere.</summary>
    private VisualProjectile CreateProjectile()
    {
        VisualProjectile projectile;

        if (projectilePrefab != null)
        {
            projectile = Instantiate(projectilePrefab, projectileContainer);
        }
        else
        {
            GameObject projectileObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            projectileObject.name = "Auto Shot Visual";
            projectileObject.transform.SetParent(projectileContainer);
            Destroy(projectileObject.GetComponent<Collider>());
            projectile = projectileObject.AddComponent<VisualProjectile>();
        }

        projectile.gameObject.SetActive(false);
        projectile.SetPoolContainer(projectileContainer);
        _createdProjectiles++;
        return projectile;
    }
}
