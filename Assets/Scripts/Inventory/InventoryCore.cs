using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCore : MonoBehaviour {
    public int inventorySpace = 6;
    public InventoryItem[] inventoryItems;
    
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

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
            // Trying to add the new inventoryItem to any already existing items which do not have full stack
            for (int i=0; i < inventoryItems.Length; i++) {                                                         // For every item in the inventory
                if (inventoryItems[i].item == null) continue;                                                       // If the item is not null
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
            for (int i=0; i < inventoryItems.Length; i++) {
                if (newInventoryItem.currentStack <= 0)
                        return new InventoryItem(null);
                
                if (newInventoryItem.currentStack > newInventoryItem.item.maxStack) {
                    if (inventoryItems[i].item == null) {
                        InventoryItem tempNewInventoryItem = new InventoryItem(newInventoryItem.item);
                        tempNewInventoryItem.currentStack = newInventoryItem.item.maxStack;
                        newInventoryItem.currentStack -= tempNewInventoryItem.currentStack;
                        inventoryItems[i] = tempNewInventoryItem;
                    }
                }
                if (newInventoryItem.currentStack <= newInventoryItem.item.maxStack) {
                    if (inventoryItems[i].item == null) {
                        inventoryItems[i] = newInventoryItem;
                        
                        if (onItemChangedCallback != null)
                            onItemChangedCallback.Invoke();

                        return new InventoryItem(null);
                    }
                }


                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
            }
            return newInventoryItem;
        }

        for (int i=0; i < inventoryItems.Length; i++) {     // For each item in inventory items
            if (inventoryItems[i].item == null) {           // If there is empty slot
                inventoryItems[i] = newInventoryItem; 
                
                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();

                return new InventoryItem(null);
            }
        }
        return newInventoryItem;
    }

    public void UpdateItem(InventoryItem inventoryItem, int index) {
        inventoryItems[index] = inventoryItem;
        
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
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