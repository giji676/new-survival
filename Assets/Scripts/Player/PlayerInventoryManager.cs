using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour {
    private Inventory inventory;
    private Hotbar hotbar;

    private void Start() {
        inventory = GetComponent<Inventory>();
        hotbar = GetComponent<Hotbar>();
    }

    /* --- OLD --- Replaced by AddHotbarFirst and AddInventoryFirst
    public InventoryItem AddItem(InventoryItem newItem) {

        // If inventory return null -> return null
        // If inventory return !null -> try hotbar
        // If hotbar return null -> return null
        // If hotbar return !null -> return item
        
        InventoryItem inventoryItem = inventory.Add(newItem); // Try adding to inventory first
        if (inventoryItem.item == null) return inventoryItem; // If inventory fully accepts it return
        
        return hotbar.Add(inventoryItem); // Else try adding to hotbar, and return the result either way
    }
    */

    public InventoryItem AddHotbarFirst(InventoryItem newItem) {
        InventoryItem inventoryItem = AddHotbarOnly(newItem);
        if (inventoryItem.item == null) return inventoryItem;
        
        return AddInventoryOnly(inventoryItem);
    }

    public InventoryItem AddInventoryFirst(InventoryItem newItem) {
        InventoryItem inventoryItem = AddInventoryOnly(newItem);
        if (inventoryItem.item == null) return inventoryItem;
        
        return AddHotbarOnly(inventoryItem);
    }

    public InventoryItem AddHotbarOnly(InventoryItem newItem) {
        return hotbar.Add(newItem);
    }
    
    public InventoryItem AddInventoryOnly(InventoryItem newItem) {
        return inventory.Add(newItem);
    }

    public InventoryItem CheckItemExists(InventoryItem inventoryItem) {
        int index = inventory.GetOnlyItemIndex(inventoryItem);
        if (index >= 0 && inventory.inventoryItems[index].currentStack >= inventoryItem.currentStack) {
            return inventory.inventoryItems[index];
        }
        return new InventoryItem(null);
    }

    public void RemoveItem(InventoryItem inventoryItem) {
        int index = inventory.GetOnlyItemIndex(inventoryItem);
        if (index < 0) return;
        if (inventory.inventoryItems[index].currentStack >= inventoryItem.currentStack) {
            InventoryItem leftOverItem = inventory.inventoryItems[index];
            leftOverItem.currentStack -= inventoryItem.currentStack;
            inventory.UpdateItem(leftOverItem, index);
        }
    }
}