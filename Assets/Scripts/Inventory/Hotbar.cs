using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : InventoryCore {
    public Transform socket;
    
    public int activeSlot = -1;
    private GameObject equipedItem;

    void Start() {
        inventoryItems = new InventoryItem[inventorySpace];
    }

    public void UseSlot(int index) {
        if (index > inventoryItems.Length) return; // Index is out of range of the hotbar slots

        if (index == activeSlot) { // If index is the same as the hotbar slot equiped
            UnequipItem();
            return;
        }

        if (activeSlot > -1) { // If there is an item already equiped at a different slot
            UnequipItem();

            if (inventoryItems[index] == null) { // If there is no item in the hotbar slot at that index
                activeSlot = -1;
                return;
            }

            EquipItem(index);
            return;
        }

        if (inventoryItems[index] != null) { // If the hotbar slot at that index has an item
            EquipItem(index);
        }
    }

    private void UnequipItem() {
        HandHeld handHeld = equipedItem.GetComponent<HandHeld>();
        if (handHeld != null) {
            handHeld.Unequip();
        }
        Destroy(equipedItem);
        activeSlot = -1;
    }

    public void UnequipAtActiveIndex() {
        equipedItem = null;
        activeSlot = -1;
    }

    private void EquipItem(int index) {
        Item item = inventoryItems[index].item;
        if (item is Equipment equipment) { // Equip it if it's equipment
            equipedItem = Instantiate(equipment.prefab, socket);
            activeSlot = index;
        }
    }
}
