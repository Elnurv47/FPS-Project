using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private const float TOP_CLAMP = -90;
    private const float BOTTOM_CLAMP = 90;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, TOP_CLAMP, BOTTOM_CLAMP);

        yRotation += mouseX;

        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
