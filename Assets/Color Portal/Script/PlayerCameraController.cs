using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] float sensitivity;
    [SerializeField] Transform player;
    [SerializeField] Transform playerCamera;
    [SerializeField] float max, min;

    void Update()
    {
        CameraHandler();
    }
    [SerializeField] Vector2 input;
    float xRot, yRot;
    void CameraHandler()
    {
        input.x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        input.y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot -= input.y;
        xRot = Mathf.Clamp(xRot, min, max);
        yRot += input.x;

        Vector3 rot = new(xRot, yRot, 0);
        playerCamera.rotation = Quaternion.Euler(rot);
        player.rotation = Quaternion.Euler(0, rot.y, 0);
    }
}
