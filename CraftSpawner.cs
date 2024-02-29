using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class CraftSpawner : MonoBehaviour
{
    public GameObject chosenCraftItem; // The object to be spawned
    public float spawnOffset = 0.1f; // Offset to ensure the spawned object is above the mesh
    [SerializeField] float spawnRadius = 10f; // Radius within which the object can be spawned
    public LayerMask layerMask; // Layer mask for raycasting
    public craftState craft; // State of crafting process
    public GameObject XRrightController; // Reference to the right controller object

 

    void Start()
    {
        // Initialize controls
        Controls.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            // Find the right controller object by tag
            XRrightController = GameObject.FindGameObjectWithTag("rightHand");
        }
        catch { }

        // State machine for crafting
        switch (craft)
        {
            case craftState.idle:
                break;

            case craftState.select:
                // Instantiate the chosen craft item at the spawner's position
                chosenCraftItem = Instantiate(chosenCraftItem, transform.position, Quaternion.identity);
                craft = craftState.craft;
                break;

            case craftState.craft:
                RaycastHit hit;
                // Cast a ray from the right controller to check for spawn position
                if (Physics.Raycast(XRrightController.transform.position, XRrightController.transform.forward, out hit, spawnRadius, layerMask))
                {
                    // Calculate spawn position with offset
                    Vector3 spawnPosition = hit.point + hit.normal * spawnOffset;
                    float smoothTime = 1; // Time to smooth movement
                    Vector3 smoothVelocity = Vector3.zero; // Initial velocity is zero
                    // Use SmoothDamp to smoothly move the craft item towards the spawn position
                    Vector3 smoothPos = Vector3.SmoothDamp(chosenCraftItem.transform.position, spawnPosition, ref smoothVelocity, smoothTime);

                    chosenCraftItem.transform.position = spawnPosition;

                    // Rotate the spawned object if the trigger is pressed
                    if (Controls.actions.XRIRightHandInteraction.B.triggered)
                    {
                        print("triggered");
                        chosenCraftItem.transform.Rotate(transform.up * 15f);
                    }

                    // Place the object if the action button is pressed
                    if (Controls.actions.XRIRightHandInteraction.A.triggered)
                    {
                        chosenCraftItem.transform.position = hit.point;
                        chosenCraftItem = null; // Reset the chosen object
                        craft = craftState.idle; // Return to idle state
                    }
                }
                break;
        }
    }
}

public enum craftState
{
    idle,   // Not actively crafting
    select, // Selecting object to craft
    craft   // Crafting the selected object
}
