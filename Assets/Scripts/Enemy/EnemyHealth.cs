using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth = 3;

    private int _currentHealth;

    /// <summary>Initializes the enemy's health from its configured maximum.</summary>
    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    /// <summary>Reduces health and removes the enemy when its health reaches zero.</summary>
    public void TakeDamage(int damage)
    {
        _currentHealth -= Mathf.Max(0, damage);
        Debug.Log($"{name} took {damage} damage. Health: {_currentHealth}/{maxHealth}", this);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
