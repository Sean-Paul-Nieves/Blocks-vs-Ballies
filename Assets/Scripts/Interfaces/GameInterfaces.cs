using UnityEngine;

public interface IMovement
{
    void Move(Vector2 direction);
}

public interface IAttack {}

// Anything that can provide a direction and a position for a projectile.
public interface IAimProvider
{
    Vector3 AimDirection { get; }
    Transform SpawnPoint { get; }
}

// Keeps shooting independent from the specific input device being used.
public interface IAttackInput
{
    bool AttackPressedThisFrame { get; }
}

public interface IDamageable
{
    void TakeDamage(int damage);
}

