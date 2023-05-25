using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour {
    private Inventory inventory;
    private Hotbar hotbar;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    private void Start() {
        inventory = GetComponent<Inventory>();
        hotbar = GetComponent<Hotbar>();
        inventory.onItemChangedCallback += OnItemCHangedUpdate;
        hotbar.onItemChangedCallback += OnItemCHangedUpdate;
    }

    public void OnItemCHangedUpdate() {
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

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

    public int GetItemCount(Item item) {
        int count = 0;
        count += inventory.ItemCount(item);
        count += hotbar.ItemCount(item);
        return count;
    }
}