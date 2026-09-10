using UnityEngine;
using UnityEngine.Pool;

public class BulletPooling : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    IObjectPool<BulletScript> _bulletPool;

    void Awake ()
    {
        _bulletPool = new ObjectPool<BulletScript>(CreateBullet, OnGet, OnRelease);
    }

    BulletScript CreateBullet()
    {
        BulletScript bullet = Instantiate(bulletPrefab).GetComponent<BulletScript>();
        bullet.SetPool(_bulletPool);
        return bullet;
    }

    void OnGet(BulletScript bullet) => bullet.gameObject.SetActive(true);
    void OnRelease(BulletScript bullet)
    {
        bullet.ResetBullet();
        bullet.gameObject.SetActive(false);
    } 

    public BulletScript GetBullet()
    {
        return _bulletPool.Get();
    }

    public void ReleaseBullet(BulletScript bullet)
    {
        _bulletPool.Release(bullet);
    }


}
