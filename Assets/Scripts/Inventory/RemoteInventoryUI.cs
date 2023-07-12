using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteInventoryUI : MonoBehaviour {
    private InventorySlot[] inventorySlots;
    public IRemoteInventory remoteInventory;

    void Start() {
        remoteInventory.inventoryCore.onItemChangedCallback += UpdateCrateUI;
        inventorySlots = GetComponentsInChildren<InventorySlot>();
        UpdateCrateUI();
    }

    void UpdateCrateUI() {
        if (remoteInventory == null) return;
        
        for (int i = 0; i < inventorySlots.Length; i++) {
            if (remoteInventory.inventoryCore.inventoryItems[i] != null) {
                if (remoteInventory.inventoryCore.inventoryItems[i].item != null) {
                    inventorySlots[i].ClearSlot();
                    inventorySlots[i].AddItem(remoteInventory.inventoryCore.inventoryItems[i]);
                }
                else {
                    inventorySlots[i].ClearSlot();
                }
            }
            else {
                inventorySlots[i].ClearSlot();
            }
        }
    }

    public void Unsubscribe() {
        remoteInventory.inventoryCore.onItemChangedCallback -= UpdateCrateUI;
    }
}