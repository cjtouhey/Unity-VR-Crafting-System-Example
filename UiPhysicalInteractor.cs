using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPhysicalInteractor : MonoBehaviour
{
    [SerializeField] CraftSpawner craft; // Reference to the CraftSpawner script
    [SerializeField] CraftSphere craftSphere; // Reference to the CraftSphere script
    PlayerControls controls; // Reference to the PlayerControls script
    public GameObject thisCraftItem; // Current craft item

    [Header("Requirements")]
    public int woodAmount; // Amount of wood required
    public int clothAmount; // Amount of cloth required
    public int scrapAmount; // Amount of scrap required

    [SerializeField] bool canSpawn; // Flag to check if crafting is possible

    List<GameObject> woodRemoval = new List<GameObject>(); // List to store wood objects to be removed
    List<GameObject> clothRemoval = new List<GameObject>(); // List to store cloth objects to be removed
    List<GameObject> scrapRemoval = new List<GameObject>(); // List to store scrap objects to be removed

    void Start()
    {
        // Find the CraftSpawner script in the scene
        craft = FindObjectOfType<CraftSpawner>();
        // Find the PlayerControls script in the scene
        controls = FindFirstObjectByType<PlayerControls>();
    }

    private void Update()
    {
        // Check if the required resources are available for crafting
        if (craftSphere.wood.Count >= woodAmount && craftSphere.cloth.Count >= clothAmount && craftSphere.scrap.Count >= scrapAmount)
        {
            canSpawn = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the index
        if (other.gameObject.CompareTag("index"))
        {
            // Check if crafting is possible
            if (canSpawn)
            {
                // Remove wood resources
                if (woodAmount > 0 && craftSphere.wood.Count >= woodAmount)
                {
                    woodRemoval.AddRange(craftSphere.wood.GetRange(0, woodAmount));
                    craftSphere.wood.RemoveRange(0, woodAmount);
                    for (int i = 0; i < woodAmount; i++)
                    {
                        Destroy(woodRemoval[i]);
                    }
                }

                // Remove cloth resources
                if (clothAmount > 0 && craftSphere.cloth.Count >= clothAmount)
                {
                    clothRemoval.AddRange(craftSphere.cloth.GetRange(0, clothAmount));
                    craftSphere.cloth.RemoveRange(0, clothAmount);
                    for (int i = 0; i < clothAmount; i++)
                    {
                        Destroy(clothRemoval[i]);
                    }
                }

                // Remove scrap resources
                if (scrapAmount > 0 && craftSphere.scrap.Count >= scrapAmount)
                {
                    scrapRemoval.AddRange(craftSphere.scrap.GetRange(0, scrapAmount));
                    craftSphere.scrap.RemoveRange(0, scrapAmount);
                    for (int i = 0; i < scrapAmount; i++)
                    {
                        Destroy(scrapRemoval[i]);
                    }
                }

                // Set the chosen craft item in CraftSpawner and trigger crafting state
                craft.chosenCraftItem = thisCraftItem;
                craft.craft = craftState.select;

                // Clear resource removal lists
                woodRemoval.RemoveRange(0, woodAmount);
                scrapRemoval.RemoveRange(0, scrapAmount);
                clothRemoval.RemoveRange(0, clothAmount);

                // Reset crafting flag
                canSpawn = false;
                // Close craft menu
                controls.openCraftMenu = false;
            }
        }
    }
}
