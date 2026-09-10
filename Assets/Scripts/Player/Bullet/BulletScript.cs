using UnityEngine;
using UnityEngine.Pool;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float bulletLifeTime = 3f;

    private float time;
    private Rigidbody rb;
    private IObjectPool<BulletScript> _bulletPool;

    private bool isReleased;

    public void SetPool(IObjectPool<BulletScript> bulletPool)
    {
        _bulletPool = bulletPool;
    }

    public void Launch(Vector3 direction)
    {
        isReleased = false;

        rb.linearVelocity = direction * speed;
        time = Time.time;
    }

    void Update()
    {
        if (Time.time >= time + bulletLifeTime)
        {
            ReleaseBullet();
        }
    }

    public void ResetBullet()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isReleased)
            return;

        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(1);
            }

            ReleaseBullet();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isReleased)
            return;

        if (collision.gameObject.CompareTag("Wall"))
        {
            ReleaseBullet();
        }
    }

    private void ReleaseBullet()
    {
        if (isReleased)
            return;

        isReleased = true;
        _bulletPool.Release(this);
    }
}