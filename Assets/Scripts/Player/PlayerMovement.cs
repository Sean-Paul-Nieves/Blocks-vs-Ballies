using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputAction _moveAction;
    
    [SerializeField] RigidbodyMovement _rbMovement;
    Vector2 _moveInput;

    void Awake() {
        _moveAction = InputSystem.actions.FindAction("Move");
        
    }

    void Update() {
        _moveInput = _moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate() {
        _rbMovement.Move(_moveInput);
    }
}
