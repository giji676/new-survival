using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountedAnimalMovement : MonoBehaviour {
    private CharacterController controller;
    [Header("Player movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float smoothInputSpeed = 0.12f;

    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;
    private Vector3 moveDirection;
    private Vector3 playerVelocity;
    
    [Header("Camera movement")]
    public float xSens = 30f;
    public float ySens = 30f;
    private float xRot = 0f;
    

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    public void ProcessMove(Vector2 input) {
        // Get player input (WASD)
        moveDirection = Vector3.zero;

        // SmoothDamp to have acceleration/deceleration
        currentInputVector = Vector2.SmoothDamp(currentInputVector, input, ref smoothInputVelocity, smoothInputSpeed);
        moveDirection.x = currentInputVector.x;
        moveDirection.z = currentInputVector.y;

        // Calculate gravity
        playerVelocity.y += gravity * Time.deltaTime;

        if (controller.isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        // Apply movement
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        controller.Move(playerVelocity * Time.deltaTime);
    }
    
    public void ProcessLook(Vector2 input, Camera cam) {
        float mouseX = input.x;
        float mouseY = input.y;

        xRot -= (mouseY * Time.deltaTime) * ySens;
        xRot = Mathf.Clamp(xRot, -80f, 80f);

        cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSens);
    }

    public void Jump() {
        if (controller.isGrounded) {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -gravity);
        }
    }
}