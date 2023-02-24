using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : InventoryCore {
    public Transform socket;
    
    public int activeSlot = -1;

    void Start() {
        inventoryItems = new InventoryItem[inventorySpace];
    }

    public void UseSlot(int index) {
        if (index < inventoryItems.Length) {
            Item  item = inventoryItems[index].item;
            if (index == activeSlot) {
                // item.Unequip();
                activeSlot = -1;
            }
            else {
                if (item is Equipment equipment) {
                    GameObject instantiatedObject = Instantiate(equipment.prefab, socket);
                    activeSlot = index;
                }
            }
        }
    }
}
