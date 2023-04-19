using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateUI : MonoBehaviour {
    // Crate
    private InventorySlot[] crateSlots;
    public Transform crateUI;
    public Crate crate;

    void Start() {
        crate.onItemChangedCallback += UpdateCrateUI;
        crateSlots = crateUI.GetComponentsInChildren<InventorySlot>();
        UpdateCrateUI();
    }

    void UpdateCrateUI() {
        if (!crate) return;

        for (int i = 0; i < crateSlots.Length; i++) {
            if (crate.inventoryItems[i] != null) {
                if (crate.inventoryItems[i].item != null) {
                crateSlots[i].ClearSlot();
                crateSlots[i].AddItem(crate.inventoryItems[i]);
                }
                else {
                    crateSlots[i].ClearSlot();
                }
            }
        }
    }
}