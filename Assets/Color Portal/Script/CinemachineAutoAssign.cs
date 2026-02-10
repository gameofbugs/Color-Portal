using Unity.Cinemachine;
using UnityEngine;

public class CinemachineAutoAssign : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("CameraPivot");
        CinemachineCamera cam = GetComponent<CinemachineCamera>();
        cam.Follow = player.transform;
        cam.LookAt = player.transform;
    }
}