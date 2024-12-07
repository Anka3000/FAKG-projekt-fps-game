using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Sensitivity Settings")]
    public float sensX = 600f;
    public float sensY = 600f;

    public Transform orientation;

    private float xRotation;
    private float yRotation;

    private bool canLook = false; // Zmienna śledząca, czy kamera może się poruszać


    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            canLook = true;
        }
        
        if(Input.GetMouseButtonUp(1))
        {
            canLook = false;
        }

        if(canLook)
        {
            // Read mouse input
            float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

            // Adjust rotations based on mouse movement
            yRotation += mouseX;
            xRotation -= mouseY;

            // Clamp the vertical rotation
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Apply rotation to camera
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);

            // Apply rotation to orientation (usually the player's body)
            if (orientation != null)
            {
                orientation.rotation = Quaternion.Euler(0, yRotation, 0);
            }
            else
            {
                Debug.LogWarning("Orientation reference is missing in CameraController script.");
            }
        }
    }

    private void LockCursor()
    {
        // Locks the cursor to the center and hides it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        // Re-lock the cursor when the application regains focus
        if (hasFocus)
        {
            LockCursor();
        }
    }
}
