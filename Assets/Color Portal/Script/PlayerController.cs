using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Base Movement Values")]
    public float baseSpeed = 5f;
    public float baseJumpForce = 5f;
    public float gravityStrength = 9.8f; // POSITIVE magnitude

    private CharacterController characterController;

    private Vector3 velocity;
    private float verticalVelocity;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        ApplyGravity();
        characterController.Move(velocity * Time.deltaTime);
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(x, 0f, z);
        velocity.x = moveDirection.x * baseSpeed;
        velocity.z = moveDirection.z * baseSpeed;

        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0f)
                verticalVelocity = -2f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = Mathf.Sqrt(2f * gravityStrength * baseJumpForce);
            }
        }

        velocity.y = verticalVelocity;
    }

    void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity -= gravityStrength * Time.deltaTime;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.TryGetComponent<Portal>(out Portal portal))
        {
            portal.Enter();
        }
        if (hit.gameObject.TryGetComponent<Crystal>(out Crystal crystal))
        {
            crystal.PlayVfx();
            GameManager.Instance.CollectCrystal(hit.gameObject);
        }
    }
}
