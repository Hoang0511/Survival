using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    // -- Is this item trashable -- //
    public bool isTrashable;

    // -- Item info UI -- //
    private GameObject itemInfoUI;
    private Text itemInfoUI_itemName;
    private Text itemInfoUI_itemDescription;
    private Text itemInfoUI_itemFunctionality;

    public string thisName, thisDescription, thisFunctionality;

    // -- Consumption -- //
    private GameObject itemPendingConsumption;
    public bool isConsumable;
    public float healthEffect;
    public float caloriesEffect;
    public float hydrationEffect;

    // -- Equip -- //
    public bool isEqippable;
    private GameObject itemPendingEquipping;
    public bool isInsideQuickSlot;

    public bool isSelected;

    public bool isUsable;


    private void Start()
    {
        itemInfoUI = InventorySystem.Instance.ItemInfoUi;
        itemInfoUI_itemName = itemInfoUI.transform.Find("ItemName").GetComponent<Text>();
        itemInfoUI_itemDescription = itemInfoUI.transform.Find("ItemDescription").GetComponent<Text>();
        itemInfoUI_itemFunctionality = itemInfoUI.transform.Find("ItemFunctionality").GetComponent<Text>();
    }



    void Update()
    {
        if(isSelected)
        {
            gameObject.GetComponent<DragDrop>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<DragDrop>().enabled = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInfoUI.SetActive(true);
        itemInfoUI_itemName.text = thisName;
        itemInfoUI_itemDescription.text = thisDescription;
        itemInfoUI_itemFunctionality.text = thisFunctionality;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoUI.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable)
            {
                // Setting this specific gameobject to be the item we want to destroy later
                itemPendingConsumption = gameObject;
                consumingFunction(healthEffect, caloriesEffect, hydrationEffect);
            }

            // quick equip //
            if (isEqippable && isInsideQuickSlot == false && EquipSystem.Instance.CheckIfFull() == false)
            {
                EquipSystem.Instance.AddToQuickSlots(gameObject);
                isInsideQuickSlot = true;
            }

            if (isUsable)
            {
                
                gameObject.SetActive(false);
                UseItem();
            }
        }



    }

  

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(isConsumable && itemPendingConsumption == gameObject)
            {
                DestroyImmediate(gameObject);
                InventorySystem.Instance.ReCalculateList();
                CraftingSystem.Instance.RefreshNeededItem();
            }
            
        }
    }

    private void UseItem()
    {

        itemInfoUI.SetActive(false);
        InventorySystem.Instance.isOpen = false;
        InventorySystem.Instance.inventoryScreenUI.SetActive(false);

        CraftingSystem.Instance.isOpen = false;
        CraftingSystem.Instance.craftingScreenUI.SetActive(false);
        CraftingSystem.Instance.toolsScreenUI.SetActive(false);
        CraftingSystem.Instance.survivalScreenUI.SetActive(false);
        CraftingSystem.Instance.refineScreenUI.SetActive(false);
        CraftingSystem.Instance.constructionScreenUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SelectionManager.Instance.EnableSelection();
        SelectionManager.Instance.enabled = true;

        switch (gameObject.name)
        {
            case "Foundation(Clone)":
                ConstructionManager.Instance.itemToBeDestroyed = gameObject;
                ConstructionManager.Instance.ActivateConstructionPlacement("FoundationModel");
                break;
            case "Foundation":
                ConstructionManager.Instance.itemToBeDestroyed = gameObject;
                ConstructionManager.Instance.ActivateConstructionPlacement("FoundationModel");
                break;
            case "Wall(Clone)":
                ConstructionManager.Instance.itemToBeDestroyed = gameObject;
                ConstructionManager.Instance.ActivateConstructionPlacement("WallModel");
                break;
            case "Wall":
                ConstructionManager.Instance.itemToBeDestroyed = gameObject;
                ConstructionManager.Instance.ActivateConstructionPlacement("WallModel");
                break;
            case "StorageBox(Clone)":
                PlacementSystem.Instance.inventoryItemToDestory = gameObject;
                PlacementSystem.Instance.ActivatePlacementMode("StorageBoxModel");
                break;
            case "StorageBox":
                PlacementSystem.Instance.inventoryItemToDestory = gameObject;
                PlacementSystem.Instance.ActivatePlacementMode("StorageBoxModel");
                break;
            case "Campfire(Clone)":
                PlacementSystem.Instance.inventoryItemToDestory = gameObject;
                PlacementSystem.Instance.ActivatePlacementMode("CampfireModel");
                break;
            case "Campfire":
                PlacementSystem.Instance.inventoryItemToDestory = gameObject;
                PlacementSystem.Instance.ActivatePlacementMode("CampfireModel");
                break;



            default:
                break;
        }


    }

    private void consumingFunction(float healthEffect, float caloriesEffect, float hydrationEffect)
    {
        itemInfoUI.SetActive(false);
        healthEffectCalculation(healthEffect);
        caloriesEffectCalculation(caloriesEffect);
        hydrationEffectCalculation(hydrationEffect);
    }

    private static void healthEffectCalculation(float healthEffect)
    {
        // -- Health -- //
        float healthBeforeConsumption = PlayerState.Instance.currentHealth;
        float maxHealth = PlayerState.Instance.maxHealth;

        if(healthEffect != 0)
        {
            if((healthBeforeConsumption + healthEffect) > maxHealth)
            {
                PlayerState.Instance.setHealth(maxHealth);
            }
            else
            {
                PlayerState.Instance.setHealth(healthBeforeConsumption + healthEffect);
            }
        }


    }

    private static void caloriesEffectCalculation(float caloriesEffect)
    {
        // -- Calories -- //
        float caloriesBeforeConsumption = PlayerState.Instance.currentCalories;
        float maxCalories = PlayerState.Instance.maxCalories;
        if (caloriesEffect != 0)
        {
            if((caloriesBeforeConsumption + caloriesEffect) > maxCalories)
            {
                PlayerState.Instance.setCalories(maxCalories);

            }
            else
            {
                PlayerState.Instance.setCalories(caloriesBeforeConsumption + caloriesEffect);
            }
        }
    }

    private static void hydrationEffectCalculation(float hydrationEffect)
    {
        // -- Hydration -- //
        float hydrationBeforeCunsumption = PlayerState.Instance.currentHydrationPercent;
        float maxHydration = PlayerState.Instance.maxHydrationPercent;
        if(hydrationEffect != 0)
        {
            if ((hydrationBeforeCunsumption + hydrationEffect) > maxHydration)
            {
                PlayerState.Instance.setHydration(maxHydration);

            }
            else
            {
                PlayerState.Instance.setHydration(hydrationBeforeCunsumption + hydrationEffect);
            }
        }
    }

}
