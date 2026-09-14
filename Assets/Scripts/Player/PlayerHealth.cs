using UnityEngine;

public sealed class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 50;
    [SerializeField] GameObject mesh;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        CurrentHealth = Mathf.Max(1, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - Mathf.Max(0, damage));

        if (CurrentHealth <= 0f)
        {
            mesh.SetActive(false);
        }
    }
}
