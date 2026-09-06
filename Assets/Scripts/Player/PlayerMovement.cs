using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Rigid Body")]
    [SerializeField] Rigidbody rb;

    [HideInInspector]
    [Header("Input System")]
    private InputAction moveAction;
    Vector3 moveInput;
    Vector3 movement;
    float movementSpeed = 15f;


    void Awake() {
        moveAction = InputSystem.actions.FindAction("Move");
        
    }

    void Update() {
        moveInput = moveAction.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        movement = new Vector3(moveInput.x, 0f, moveInput.y);

        rb.linearVelocity = movement * movementSpeed;
    }
}
