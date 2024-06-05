using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    public bool playerInRange;

    public string ItemName;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerInRange && SelectionManager.Instance.onTarget && SelectionManager.Instance.selectedObject == gameObject)
        {
            if(InventorySystem.Instance.CheckSlotAvailable(1))
            {
                InventorySystem.Instance.AddToInventory(ItemName);

                InventorySystem.Instance.itemsPickedup.Add(gameObject.name);

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("inventory is full");
            }
            
        }
    }
    public string GetItemName()
    {
        return ItemName;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;  
        }
    }
}
