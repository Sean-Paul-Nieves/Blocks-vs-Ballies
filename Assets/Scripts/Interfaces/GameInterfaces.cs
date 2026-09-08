using UnityEngine;

public interface IMovement
{
    void Move(Vector2 direction);
}

public interface IAttack {}

public interface IDamageable
{
    void TakeDamage(int damage);
}
