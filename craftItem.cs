using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craftItem : MonoBehaviour
{
    MeshRenderer mesh; // Mesh renderer component for the object

    public Material[] materials; // Array of materials for the object

    Material defaultMaterial; // Default material of the object
    [SerializeField] Material highlightMaterial; // Material used for highlighting

    [SerializeField] hightlightState state; // Current highlight state of the object

    // Enum defining highlight states
    public enum hightlightState
    {
        unlit, // Object is not highlighted
        lit    // Object is highlighted
    }

    void Start()
    {
        // Get the mesh renderer component
        mesh = GetComponent<MeshRenderer>();
        // Store the default material
        defaultMaterial = mesh.material;
    }

    private void Update()
    {
        // Switch statement to handle different highlight states
        switch (state)
        {
            case hightlightState.unlit:
                // Set the material to default when unlit
                mesh.material = defaultMaterial;
                break;

            case hightlightState.lit:
                // Set the material to highlight material when lit
                mesh.material = highlightMaterial;
                break;
        }
    }
}
