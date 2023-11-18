using UnityEngine;
using TMPro;

public class CursorCoordinates : MonoBehaviour
{
    private TextMeshProUGUI coordinatesText; // Reference to the TextMeshPro object

    void Start()
    {
        // Find the TextMeshPro object within the hierarchy
        coordinatesText = GetComponentInChildren<TextMeshProUGUI>();

        // Ensure coordinatesText is not null
        if (coordinatesText == null)
        {
            Debug.LogError("TextMeshPro object not found!");
            enabled = false; // Disable this script if the TextMeshPro object is not found
        }
    }

    void Update()
    {
        // Get the cursor position in world coordinates
        Vector3 cursorPosition = transform.position;

        // Update the position of the text to follow the cursor
        coordinatesText.transform.position = cursorPosition + Vector3.up * 0.5f; // Adjust the offset as needed

        // Display the cursor's coordinates on the TextMeshPro object
        coordinatesText.text = $"X: {cursorPosition.x:F2}, Y: {cursorPosition.y:F2}";
    }
}
