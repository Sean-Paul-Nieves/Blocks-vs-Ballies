using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth = 3;

    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= Mathf.Max(0, damage);
        SoundManager.PlaySound(SoundType.ENEMYHIT, 0.6f);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
            SoundManager.PlaySound(SoundType.ENEMYDEAD, 0.7f);
        }
    }
}
