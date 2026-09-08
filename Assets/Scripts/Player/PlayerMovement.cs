using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputAction _moveAction;
    
    [SerializeField] RigidbodyMovement _rbMovement;
    Vector2 _moveInput;


    /// <summary>Finds the configured movement action when the component is initialized.</summary>
    void Awake() {
        _moveAction = InputSystem.actions.FindAction("Move");
        
    }

    /// <summary>Reads the latest movement input so it can be applied during the next physics step.</summary>
    void Update() {
        _moveInput = _moveAction.ReadValue<Vector2>();
    }

    /// <summary>Passes the cached movement input to the Rigidbody movement component on the physics tick.</summary>
    void FixedUpdate() {
        _rbMovement.Move(_moveInput);
    }
}
