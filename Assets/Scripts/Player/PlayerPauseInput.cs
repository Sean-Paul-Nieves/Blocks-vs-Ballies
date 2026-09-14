using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPauseInput : MonoBehaviour
{
    private InputAction pauseAction;
    [SerializeField] GameObject pausePanel;

    void Awake()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
    }

    void OnEnable()
    {
        pauseAction.performed += OnPause;
    }

    void OnDisable()
    {
        pauseAction.performed -= OnPause;
    }

    void OnPause(InputAction.CallbackContext context)
    {
        pausePanel.SetActive(!pausePanel.activeInHierarchy);
    }
}
