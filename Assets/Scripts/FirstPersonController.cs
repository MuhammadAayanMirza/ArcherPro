using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Transform playerCamera;
    public CharacterController controller;
    public float mouseSensitivity = 150f;
    public float xRotation = 0f;
    public float verticalVelocity = 0f;

    public float gravity = -9.81f;

    public float jumpDistance = 3f;

    public void Start()
    {
        Debug.Log("FPC Begins...");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 

        controller = GetComponent<CharacterController>();
    }

    public void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveY;

        mouseX *= mouseSensitivity * Time.deltaTime;
        mouseY *= mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

        Vector3 moveQuantity = moveDirection * moveSpeed * Time.deltaTime;

        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = Mathf.Sqrt(jumpDistance * -2f * gravity);
        }

        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }
        
        verticalVelocity = verticalVelocity + gravity * Time.deltaTime;

        Vector3 verticalMove = Vector3.up * verticalVelocity * Time.deltaTime;

        moveQuantity += verticalMove;

        controller.Move(moveQuantity);

        
    }

















}
