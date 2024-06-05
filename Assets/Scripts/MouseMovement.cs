using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    float xRotation = 0;
    float YRotation = 0;

    void Start()
    {
         Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        if (!InventorySystem.Instance.isOpen && !CraftingSystem.Instance.isOpen && !MenuManager.Instance.isMenuOpen
            && !StorageManager.Instance.storageUIOpen && !CampfireUIManager.Instance.isUiOpen)
            
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            YRotation += mouseX;

            transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
        }
        
    }
}
