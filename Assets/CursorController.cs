using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Grid gridReference;
    public Transform movePoint;
    public float moveSpeed = 5f;

    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        HandleMovement();

        // Check for character selection when 'X' key is pressed

        if (Input.GetKeyDown(KeyCode.X))
        {
            CharData selectedCharacter = GetSelectedCharacter();

            if (selectedCharacter != null)
            {
                TryMoveSelectedCharacter(selectedCharacter);
            }
            else
            {
                SelectCharacterAtCursor();
            }
        }
    }

    void HandleMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.01f)
        {
            HandleInputMovement();
        }
    }

    void HandleInputMovement()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
        {
            movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
        }

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
        {
            movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
        }
    }

    void TryMoveSelectedCharacter(CharData selectedCharacter)
    {
        // Assuming you have a reference to your grid
        Grid grid = gridReference; // Replace with your actual grid reference

        Vector3 cursorPosition = transform.position; // Read the cursor position

        // Convert the cursor's world position to a grid cell position
        Vector3Int cellPosition = grid.WorldToCell(cursorPosition);

        // Check if the cursor is on an empty square (no character at the cursor position)
        bool isCursorOnEmptySquare = true;
        CharData[] characters = FindObjectsOfType<CharData>();
        foreach (CharData character in characters)
        {
            if (character != selectedCharacter && grid.WorldToCell(character.transform.position) == cellPosition)
            {
                isCursorOnEmptySquare = false;
                break;
            }
        }

        if (isCursorOnEmptySquare)
        {
            // Move the selected character to the grid cell position
            selectedCharacter.MoveTo(cellPosition);
        }
    }

    void SelectCharacterAtCursor()
    {
        Vector3 cursorPosition = transform.position; // Read the cursor position

        // Get all character objects in the scene
        CharData[] characters = FindObjectsOfType<CharData>();

        // Check if any character is at the cursor position
        foreach (CharData character in characters)
        {
            if (character.transform.position == cursorPosition)
            {
                character.SetSelected(true);
                break;
            }
        }
    }

    CharData GetSelectedCharacter()
    {
        CharData[] characters = FindObjectsOfType<CharData>();

        // Find the selected character
        foreach (CharData character in characters)
        {
            if (character.isSelected)
            {
                return character;
            }
        }

        return null; // If no character is selected
    }
}