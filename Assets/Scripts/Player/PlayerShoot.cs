using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.ParticleSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private PlayerAttackInput attackInputComponent;
    [SerializeField] private BulletPooling bulletPool;
    [SerializeField] private PlayerAim aimComponent;
    [SerializeField] Animator animator;

    private IAttackInput attackInput;
    private IAimProvider aimProvider;

    void Awake()
    {
        // Inspector fields remain Unity-friendly; gameplay code uses interfaces.
        attackInput = attackInputComponent;
        aimProvider = aimComponent;
    }

    void Update()
    {
        if (attackInput != null && attackInput.AttackPressedThisFrame)
        {
            Fire();
        }
    }

    void Fire()
    {
        if (bulletPrefab == null || aimProvider == null)
        {
            return;
        }

        BulletScript bullet = bulletPool.GetBullet();

        bullet.transform.position = aimProvider.SpawnPoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(aimProvider.AimDirection);

        bullet.Launch(aimProvider.AimDirection);

        SoundManager.PlaySound(SoundType.SHOOT, 0.3f);
        animator.SetTrigger("Shoot");
    }
}

