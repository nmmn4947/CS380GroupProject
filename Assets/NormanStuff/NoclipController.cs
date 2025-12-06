using UnityEngine;

[DisallowMultipleComponent]
public class NoclipController : MonoBehaviour
{
    [Header("Mouse Look")]
    public float mouseSensitivity = 10f;
    public bool invertY = false;
    public float smoothTime = 0.03f;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float acceleration = 20f;
    public bool useLocalSpace = true;

    [Header("Vertical Control")]
    public KeyCode ascendKey = KeyCode.Space;
    public KeyCode descendKey = KeyCode.LeftShift; // changed here

    [Header("Cursor")]
    public bool lockCursor = true;
    public KeyCode toggleCursorKey = KeyCode.Escape;

    float pitch = 0f;
    float yaw = 0f;
    Vector3 currentVelocity = Vector3.zero;
    Vector2 rotVelocity = Vector2.zero;

    void Start()
    {
        Vector3 e = transform.eulerAngles;
        yaw = e.y;
        pitch = e.x;

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        HandleCursorLockToggle();
        HandleMouseLook();
        HandleMovement();
    }

    void HandleCursorLockToggle()
    {
        if (Input.GetKeyDown(toggleCursorKey))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void HandleMouseLook()
    {
        if (Cursor.lockState != CursorLockMode.Locked) 
            return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        if (invertY) mouseY = -mouseY;

        // update your own angles (never read from transform.eulerAngles!)
        yaw += mouseX;
        pitch -= mouseY;

        pitch = Mathf.Clamp(pitch, -89f, 89f);

        // directly apply rotation — no smoothing with Euler angles
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }


    void HandleMovement()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        Vector3 move = Vector3.zero;

        if (useLocalSpace)
        {
            Vector3 forward = transform.forward;
            forward = Vector3.ProjectOnPlane(forward, Vector3.up).normalized;

            Vector3 right = transform.right;
            right = Vector3.ProjectOnPlane(right, Vector3.up).normalized;

            move = right * inputX + forward * inputZ;
        }
        else
        {
            move = new Vector3(inputX, 0f, inputZ);
        }

        if (Input.GetKey(ascendKey)) move += Vector3.up;
        if (Input.GetKey(descendKey)) move += Vector3.down; // now Left Shift

        move = move.normalized;
        Vector3 targetVelocity = move * moveSpeed;

        currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        transform.position += currentVelocity * Time.deltaTime;
    }
}
