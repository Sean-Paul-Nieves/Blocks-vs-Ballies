using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour, IAimProvider
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Vector3 direction = Vector3.forward;

    public Vector3 AimDirection => direction;
    public Transform SpawnPoint => bulletSpawn;

    void Update()
    {
        UpdateAim();
    }

    public void UpdateAim()
    {
        Camera aimCamera = mainCamera != null ? mainCamera : Camera.main;

        if (Mouse.current == null || aimCamera == null || bulletSpawn == null)
        {
            return;
        }

        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = aimCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            Vector3 newDirection = hit.point - bulletSpawn.position;

            // Ignore a target point that is exactly at the projectile spawn point.
            if (newDirection.sqrMagnitude < 0.001f)
            {
                return;
            }

            direction = newDirection.normalized;

            bulletSpawn.rotation = Quaternion.LookRotation(direction);
        }
    }
}