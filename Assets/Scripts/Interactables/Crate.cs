using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Interactable {
    public GameObject crateInventoryUI;
    public InventoryCore inventoryCore;
    
    protected override void Interact(GameObject interactingObject, InteractionType interactionType) {
        base.Interact(interactingObject, interactionType);
        CrateManager crateManager = interactingObject.GetComponent<CrateManager>();

        if (!crateManager) return;
        if (crateManager.crateAccessed) return;

        crateManager.crateAccessed = true;
        crateManager.crate = this;
        GameObject playerUI = crateManager.inventoryUI;
        GameObject inventoryUIInstance = Instantiate(crateInventoryUI);
        inventoryUIInstance.GetComponent<CrateUI>().crate = this;
        inventoryUIInstance.transform.SetParent(playerUI.transform, false);
        crateManager.newUI = inventoryUIInstance;
        crateManager.SetInventoryActive();
    }

    void Start() {
        inventoryCore = GetComponent<InventoryCore>();
    }
}