using UnityEngine;
using UnityEngine.InputSystem;

// This class owns input detection. PlayerShoot does not need to know whether
// the attack came from a mouse, controller, keyboard, or another input source.
public class PlayerAttackInput : MonoBehaviour, IAttackInput
{
    private InputAction attackAction;

    public bool AttackPressedThisFrame => attackAction != null && attackAction.triggered;

    void Awake()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
    }
}