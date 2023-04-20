using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateUI : MonoBehaviour {
    // Crate
    private InventorySlot[] crateSlots;
    public Crate crate;

    void Start() {
        crate.inventoryCore.onItemChangedCallback += UpdateCrateUI;
        crateSlots = GetComponentsInChildren<InventorySlot>();
        UpdateCrateUI();
    }

    void UpdateCrateUI() {
        if (!crate) return;

        for (int i = 0; i < crateSlots.Length; i++) {
            if (crate.inventoryCore.inventoryItems[i] != null) {
                if (crate.inventoryCore.inventoryItems[i].item != null) {
                crateSlots[i].ClearSlot();
                crateSlots[i].AddItem(crate.inventoryCore.inventoryItems[i]);
                }
                else {
                    crateSlots[i].ClearSlot();
                }
            }
            else {
                crateSlots[i].ClearSlot();
            }
        }
    }

    public void Unsubscribe() {
        crate.inventoryCore.onItemChangedCallback -= UpdateCrateUI;
    }
}