using UnityEngine;

public class RigidbodyMovement : MonoBehaviour, IMovement
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;
    float _movementSpeed = 15f;
    
    public void Move(Vector2 direction)
    {
        var _movement = new Vector3(direction.x, 0f, direction.y);

        rb.linearVelocity = _movement * _movementSpeed;
        animator.SetFloat("Walk", _movement.magnitude);
    }
}
