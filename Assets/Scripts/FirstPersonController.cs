using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Transform playerCamera;
    public float mouseSensitivity = 150f;
    public float xRotation = 0f;

    public void Start()
    {
        Debug.Log("Player Controller started");
    }

    public void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        mouseX *= mouseSensitivity * Time.deltaTime;
        mouseY *= mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

    }


















}
