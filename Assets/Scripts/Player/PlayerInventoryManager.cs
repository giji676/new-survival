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

    public InventoryItem AddItem(InventoryItem newItem) {

        // If inventory return null -> return null
        // If inventory return !null -> try hotbar
        // If hotbar return null -> return null
        // If hotbar return !null -> return item
        
        InventoryItem inventoryItem = inventory.Add(newItem); // Try adding to inventory first
        if (inventoryItem.item == null) return inventoryItem; // If inventory fully accepts it return
        
        return hotbar.Add(inventoryItem); // Else try adding to hotbar, and return the result either way
    }
}