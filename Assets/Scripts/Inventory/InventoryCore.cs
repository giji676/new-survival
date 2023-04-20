using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCore : MonoBehaviour {
    public int inventorySpace = 6;
    public InventoryItem[] inventoryItems;
    
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int itemCount = 0;

    void Start() {
    }
    
    public InventoryItem Add(InventoryItem newInventoryItem) {
        FixList();
        /*
        If item is stackable
            If item exists in the inventory:
                If item stack is less than maximum stack:
                    Add as much as you can:
                        Move onto another slot with the remainder stack
                Check another slot
            Add it to the inventory
        */
        if (newInventoryItem.item.stackable) {                                                                      // If item is stackable
            for (int i=0; i < inventoryItems.Length; i++) {                                                         // For every item in the inventory
                if (inventoryItems[i].item == null) continue;
                if (inventoryItems[i].item.name == newInventoryItem.item.name) {                                    // If the inventory name matches item name
                    if (inventoryItems[i].currentStack < inventoryItems[i].item.maxStack) {                         // If the inventory item current stack is less than maximum stack
                        int emptyStackAvailable = newInventoryItem.item.maxStack - inventoryItems[i].currentStack;  // How much more the item can stack in the inventory
                        if (newInventoryItem.currentStack <= emptyStackAvailable) {                                 // If the items currentstack is less than what the inventory item can stack
                            inventoryItems[i].currentStack += newInventoryItem.currentStack;                        // Increase the inventory item current stack with empty stack available

                            if (onItemChangedCallback != null)
                                onItemChangedCallback.Invoke();

                            return new InventoryItem(null);
                        }
                        else {                                                      // If the items current stack is more than what the inventory item can stack
                            newInventoryItem.currentStack -= emptyStackAvailable;   // Decrease the items current stack with the available
                            inventoryItems[i].currentStack += emptyStackAvailable;  // Increase the inventory item stack with the available
                            
                            if (onItemChangedCallback != null)
                                onItemChangedCallback.Invoke();
                        }
                    }
                }
            }
            if (newInventoryItem.currentStack <= newInventoryItem.item.maxStack) {  // If items current stack is less or equal to items max stack
                for (int i=0; i < inventoryItems.Length; i++) {                     // For each item in inventory items
                    if (inventoryItems[i].item == null) {                                // If there is empty slot
                        inventoryItems[i] = newInventoryItem;                       // Add it as a new item in inventory item list
                        itemCount += 1;
                        
                        if (onItemChangedCallback != null)
                            onItemChangedCallback.Invoke();

                        return new InventoryItem(null);
                    }
                }
            }
            else {                                                                          // If the current stack is more than items max stack
                for (int i=0; i < inventoryItems.Length; i++) {                             // For each item in inventory items
                    if (inventoryItems[i].item == null) {                                        // If there is empty slot
                        InventoryItem tempNewInventoryItem = newInventoryItem;              // Create a new inventory item - 
                        tempNewInventoryItem.currentStack = newInventoryItem.item.maxStack; // With max stack size
                        newInventoryItem.currentStack -= newInventoryItem.item.maxStack;    // Decrease the original inventory item with the same amount
                        inventoryItems[i] = tempNewInventoryItem;                           // Set the inventory item to the newly create invetory item
                        itemCount += 1;
                        
                        if (onItemChangedCallback != null)
                            onItemChangedCallback.Invoke();
                        
                        Add(newInventoryItem);                                              // Try adding the left over inventory item again to the inventory
                    }
                }
            }
        }

        for (int i=0; i < inventoryItems.Length; i++) {     // For each item in inventory items
            if (inventoryItems[i].item == null) {                // If there is empty slot
                inventoryItems[i] = newInventoryItem; 
                itemCount += 1;
                
                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();

                return new InventoryItem(null);
            }
        }
        return newInventoryItem;
    }

    private void FixList() {
        for (int i=0; i < inventoryItems.Length; i++) {
            if (inventoryItems[i] == null) {
                inventoryItems[i] = new InventoryItem(null);
            }
        }
    }

    public void Remove(int index) {
        inventoryItems[index] = new InventoryItem(null);
        itemCount -= 1;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public int GetItemIndex(InventoryItem inventoryItem) {
        for (int i=0; i < inventoryItems.Length; i++) {
            if (inventoryItem == inventoryItems[i]) return i;
        }
        return -1;
    }
}