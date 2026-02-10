using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] float sensitivity = 100f;
    [SerializeField] Transform player;
    [SerializeField] Transform playerCamera;
    [SerializeField] float max = 80f;
    [SerializeField] float min = -80f;

    [SerializeField] Vector2 input;
    float xRot, yRot;

    void Start()
    {
        // Auto-find references if not set
        if (player == null)
        {
            player = transform.parent; // Assuming this script is on a child of player
        }

        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>()?.transform;
        }

        // Lock cursor for better camera control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Only control camera when playing
        if (GameManager.Instance != null &&
            GameManager.Instance.CurrentGameState() != GameState.Playing)
        {
            return;
        }

        CameraHandler();
    }

    void CameraHandler()
    {
        if (player == null || playerCamera == null) return;

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, min, max);
        yRot += mouseX;

        playerCamera.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        player.rotation = Quaternion.Euler(0f, yRot, 0f);
    }

}