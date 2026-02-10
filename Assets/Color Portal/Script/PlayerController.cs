using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;
    public Animator animator;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = 9.8f;
    public float rotationSpeed = 10f;

    private CharacterController characterController;
    private Vector3 moveVelocity;
    private float verticalVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!CanMove())
        {
            ResetVelocity();
            return;
        }

        HandleMovement();
        ApplyGravity();
        characterController.Move(moveVelocity * Time.deltaTime);
    }

    bool CanMove()
    {
        return GameManager.Instance != null &&
               GameManager.Instance.CurrentGameState() == GameState.Playing;
    }

    void ResetVelocity()
    {
        moveVelocity = Vector3.zero;
        verticalVelocity = 0f;
    }

    void HandleMovement()
    {
        // Get input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate camera-relative movement direction
        Vector3 cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 cameraRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;

        Vector3 moveDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

        // Apply horizontal movement
        moveVelocity.x = moveDirection.x * moveSpeed;
        moveVelocity.z = moveDirection.z * moveSpeed;

        // Rotate player to face movement direction
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Handle jumping
        if (characterController.isGrounded)
        {
            verticalVelocity = verticalVelocity < 0f ? -2f : verticalVelocity;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = Mathf.Sqrt(2f * gravity * jumpHeight);

                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayPlayerJump();
                }
            }
        }

        moveVelocity.y = verticalVelocity;

        // Update animator
        UpdateAnimator(horizontal, vertical);
    }

    void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
    }

    void UpdateAnimator(float horizontal, float vertical)
    {
        if (animator == null) return;

        float animationSpeed = new Vector3(horizontal, 0f, vertical).magnitude;
        animator.SetFloat("Speed", animationSpeed);
        animator.SetBool("IsOnGround", characterController.isGrounded);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Handle portal collision
        if (hit.gameObject.TryGetComponent<Portal>(out Portal portal))
        {
            portal.Enter();
        }

        // Handle crystal collection
        if (hit.gameObject.TryGetComponent<Crystal>(out Crystal crystal))
        {
            crystal.PlayVfx();

            if (GameManager.Instance != null)
            {
                GameManager.Instance.CollectCrystal(hit.gameObject);
            }
        }

        // Handle interactor collision (game over trigger)
        if (hit.gameObject.TryGetComponent<Interactor>(out Interactor interactor))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOverLogic();
            }
        }
    }
}