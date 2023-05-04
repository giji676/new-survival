using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour {
    private PlayerInventoryManager inventoryManager;
    public Item testItem;
    private void Start() {
        inventoryManager = GetComponent<PlayerInventoryManager>();
    }

    private void Update() {
        TryCraftItem(testItem);
    }

    public void TryCraftItem(Item item) {
        if (!item.isCraftable) return;
        if (item.craftingRecipe.Count == 0) return;

        foreach (InventoryItem inventoryItem in item.craftingRecipe) {
            InventoryItem returned = inventoryManager.CheckItemExists(inventoryItem);
            if (returned.item == null)
                return;
        }

        foreach (InventoryItem inventoryItem in item.craftingRecipe) {
            inventoryManager.RemoveItem(inventoryItem);
        }

        InventoryItem craftedItem = new InventoryItem(item);
        craftedItem.currentStack = item.returnAmount;
        inventoryManager.AddInventoryFirst(craftedItem);
    }
}