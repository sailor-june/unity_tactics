// using UnityEngine;

// public class CharacterHighlight : MonoBehaviour
// {
//     public Material outlineMaterial; // Reference to your outline material
//     private SpriteRenderer spriteRenderer;
//     private Material originalMaterial;

//     void Start()
//     {
//         // Accessing the SpriteRenderer component from a child object named "CharacterSprite"
//         Transform characterSprite = transform.Find("CharacterSprite");

//         if (characterSprite != null)
//         {
//             spriteRenderer = characterSprite.GetComponent<SpriteRenderer>();
//             if (spriteRenderer != null)
//             {
//                 originalMaterial = spriteRenderer.material;
//             }
//             else
//             {
//                 Debug.LogError("SpriteRenderer component not found in the child object.");
//             }
//         }
//         else
//         {
//             Debug.LogError("Child object named 'CharacterSprite' not found.");
//         }
//     }

//     void Update()
//     {
//         if character.isSelected == true
//         {
//             ApplyOutlineEffect();
//         }
//     }

  

//     void ApplyOutlineEffect()
//     {
//         if (outlineMaterial != null && spriteRenderer != null)
//         {
//             spriteRenderer.material = outlineMaterial;
//         }
//     }

//     // Additional methods for removing the outline effect if needed
// }
