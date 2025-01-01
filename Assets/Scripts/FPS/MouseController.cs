using UnityEngine;

public class MouseController : MonoBehaviour
{
    private const float TOP_CLAMP = -90;
    private const float BOTTOM_CLAMP = 90;
    private const string MOUSE_X_INPUT_NAME = "Mouse X";
    private const string MOUSE_Y_INPUT_NAME = "Mouse Y";

    private float xRotation = 0f;
    private float yRotation = 0f;

    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis(MOUSE_X_INPUT_NAME) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(MOUSE_Y_INPUT_NAME) * mouseSensitivity * Time.deltaTime;

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
