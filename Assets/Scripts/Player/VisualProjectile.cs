using System;
using UnityEngine;

/// <summary>Moves one cosmetic projectile and reports completion to its pool.</summary>
public sealed class VisualProjectile : MonoBehaviour
{
    private static readonly int ColorProperty = Shader.PropertyToID("_Color");
    private static readonly int BaseColorProperty = Shader.PropertyToID("_BaseColor");

    private Transform _target;
    private Vector3 _fallbackTargetPosition;
    private float _speed;
    private float _arrivalDistanceSqr;
    private Renderer _projectileRenderer;
    private MaterialPropertyBlock _propertyBlock;
    private Action<VisualProjectile> _release;

    /// <summary>Caches renderer state once instead of looking it up for every shot.</summary>
    private void Awake()
    {
        _projectileRenderer = GetComponent<Renderer>();
        _propertyBlock = new MaterialPropertyBlock();
    }

    /// <summary>Stores the pool container used to keep runtime projectile objects organized.</summary>
    public void SetPoolContainer(Transform container)
    {
        transform.SetParent(container);
    }

    /// <summary>Initializes a pooled projectile without creating materials or GameObjects.</summary>
    public void Launch(Transform target, float speed, float size, Color color, Action<VisualProjectile> release)
    {
        _target = target;
        _fallbackTargetPosition = target != null ? target.position : transform.position;
        _speed = Mathf.Max(0f, speed);
        _arrivalDistanceSqr = 0.01f * 0.01f;
        _release = release;
        transform.localScale = Vector3.one * Mathf.Max(0.001f, size);

        _projectileRenderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetColor(ColorProperty, color);
        _propertyBlock.SetColor(BaseColorProperty, color);
        _projectileRenderer.SetPropertyBlock(_propertyBlock);
    }

    /// <summary>Moves toward the live target and returns the projectile when it reaches it.</summary>
    public void Tick(float deltaTime)
    {
        Vector3 targetPosition = _target != null
            ? _target.position
            : _fallbackTargetPosition;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            _speed * deltaTime);

        if ((transform.position - targetPosition).sqrMagnitude <= _arrivalDistanceSqr)
        {
            _release?.Invoke(this);
        }
    }

    /// <summary>Clears per-shot state before the projectile is stored for reuse.</summary>
    public void ResetForPool()
    {
        _target = null;
        _fallbackTargetPosition = Vector3.zero;
        _speed = 0f;
        _release = null;
    }
}
