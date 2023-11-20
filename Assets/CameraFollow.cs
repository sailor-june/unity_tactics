using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cursor; // Reference to the cursor object
    public float followSpeed = 5f; // Adjust the speed of the camera follow

    void Update()
    {
        if (cursor != null)
        {
            Vector3 cursorPos = cursor.position;
            cursorPos.z = transform.position.z; // Maintain the camera's original Z position
            transform.position = Vector3.Lerp(transform.position, cursorPos, followSpeed * Time.deltaTime);
        }
    }
}