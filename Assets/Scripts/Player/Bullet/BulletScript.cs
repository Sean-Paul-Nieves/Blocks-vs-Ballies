using UnityEngine;
using UnityEngine.Pool;
public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody rb;
    IObjectPool<BulletScript> _bulletPool;

    public void SetPool (IObjectPool<BulletScript> bulletPool){
        _bulletPool = bulletPool;
    }

    public void Launch(Vector3 direction)
    {
        rb.linearVelocity = direction * speed;
    }

    public void ResetBullet()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(1);
            }
            _bulletPool.Release(this);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Wall"))
            _bulletPool.Release(this);
    }
}
