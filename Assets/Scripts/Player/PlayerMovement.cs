using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string HORİZONTAL_AXİS_INPUT = "Horizontal";
    private const string VERTİCAL_AXİS_INPUT = "Vertical";
    private const string JUMP_INPUT = "Jump";

    private bool isGrounded;

    [Header("Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -15f;
    [Header("Ground Check Properties")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;

    public Action<bool> OnMovementStateChanged;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleCharacterMovement();
        HandleJumpingAndGravity();
    }

    private void HandleJumpingAndGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetButtonDown(JUMP_INPUT) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleCharacterMovement()
    {
        Vector3 move = GetMoveVectorFromInput();

        bool isWalking = move.magnitude > 0.5f;
        OnMovementStateChanged?.Invoke(isWalking);

        controller.Move(move * speed * Time.deltaTime);
    }

    private Vector3 GetMoveVectorFromInput()
    {
        float moveX = Input.GetAxis(HORİZONTAL_AXİS_INPUT);
        float moveZ = Input.GetAxis(VERTİCAL_AXİS_INPUT);

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        return move;
    }
}
