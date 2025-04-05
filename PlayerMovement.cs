using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Camera Settings")] 
    public Camera playerCamera;
    public float zoomSpeed = 2f;
    public float minZoom = 2f;
    public float maxZoom = 10f;

    private Rigidbody rb;
    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        // Get WASD input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal, 0f, vertical).normalized;

        // Handle camera zoom
        if (Input.GetKey(KeyCode.Z))
        {
            ZoomCamera(1);
        }
        if (Input.GetKey(KeyCode.X))
        {
            ZoomCamera(-1);
        }
    }

    void FixedUpdate()
    {
        // Move the player
        if (movement.magnitude >= 0.1f)
        {
            Vector3 moveDirection = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0) * movement;
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

            // Rotate player to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    void ZoomCamera(float direction)
    {
        float newZoom = playerCamera.fieldOfView + (direction * zoomSpeed);
        playerCamera.fieldOfView = Mathf.Clamp(newZoom, minZoom, maxZoom);
    }
}