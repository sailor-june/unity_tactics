using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharData : MonoBehaviour
{
    private float moveSpeed = 5f;

    public Grid grid;
    public bool isSelected = false;
    private Pathfinding pathfinding; // Reference to the Pathfinding script

    private List<Vector3Int> currentPath;
    private int currentPathIndex;
      private Material originalMaterial;
    public Material outlineMaterial;
    void Start()
    {
        // Get a reference to the Pathfinding script
        pathfinding = FindObjectOfType<Pathfinding>();
    }

    public void SetSelected(bool value)
    {
        isSelected = value;
        Renderer renderer = GetComponentInChildren<Renderer>();

        if (isSelected)
        {
            // Apply outline material
            originalMaterial = renderer.material;
            renderer.material = outlineMaterial;
        }
        else
        {
            // Revert to the original material
            if (originalMaterial != null)
            {
                renderer.material = originalMaterial;
            }
        }
    }

    // public void SetSelected(bool value)
    // {
    //     isSelected = value;
    //     if (outlineMaterial != null)
    //     {
    //         if (isSelected)
    //         {
    //             // Apply outline shader
    //             GetComponentInChildren<SpriteRenderer>().material = outlineMaterial;
    //         }
    //         else
    //         {
    //             // Remove outline shader
    //             GetComponentInChildren<SpriteRenderer>().material = null; // Or revert to the original material
    //         }
    //     }
    // }

    public void MoveTo(Vector3Int target)
    {
        currentPath = pathfinding.FindPath(grid.WorldToCell(transform.position), target);
        currentPathIndex = 0;
        Debug.Log("Path: ");
        foreach (Vector3Int pos in currentPath)
        {
            Debug.Log("X: " + pos.x + ", Y: " + pos.y + ", Z: " + pos.z);
        }
;
        if (currentPath != null && currentPath.Count > 0)
        {
            StartCoroutine(FollowPath());
        }
        
    }

    private IEnumerator FollowPath()
    {
        while (currentPathIndex < currentPath.Count)
        {
            Vector3Int targetPosition = currentPath[currentPathIndex];
            Vector3 targetWorldPosition = grid.CellToWorld(targetPosition);

            while (Vector3.Distance(transform.position, targetWorldPosition) > 0.01f)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetWorldPosition, step);
                yield return null;
            }

            transform.position = targetWorldPosition;
            currentPathIndex++;
            yield return new WaitForSeconds(0.05f);
        }
    SetSelected(false);
    }
}
