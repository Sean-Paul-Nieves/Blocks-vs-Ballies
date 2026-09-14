using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth = 3;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem hitParticle;

    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= Mathf.Max(0, damage);
        SoundManager.PlaySound(SoundType.ENEMYHIT, 0.6f);
        animator.SetTrigger("Hit");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(1);
            Die();
        }
    }

    void Die()
    {
        hitParticle.Play();
        Destroy(gameObject);
        SoundManager.PlaySound(SoundType.ENEMYDEAD, 0.7f);
    }
}
