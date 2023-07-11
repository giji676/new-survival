using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : Interactable, IRemoteInventory {
    [SerializeField] private GameObject _remoteInventoryUI;
    [SerializeField] private InventoryCore _inventoryCore;

    public GameObject remoteInventoryUI {
        get { return _remoteInventoryUI; }
        set { _remoteInventoryUI = value; }
    }

    public InventoryCore inventoryCore {
        get { return _inventoryCore; }
        set { _inventoryCore = value; }
    }

    protected override void Interact(GameObject interactingObject, InteractionType interactionType) {
        base.Interact(interactingObject, interactionType);
        Access(interactingObject, interactionType);
    }

    public void Access(GameObject interactingObject, InteractionType interactionType) {

        // Custom implementation for Interact in Furnace class
        RemoteInventoryManager remoteInventoryManager = interactingObject.GetComponent<RemoteInventoryManager>();

        if (!remoteInventoryManager) return;
        if (remoteInventoryManager.remoteInventoryAccessed) return;

        remoteInventoryManager.remoteInventoryAccessed = true;
        remoteInventoryManager.remoteInventory = this;
        GameObject playerUI = remoteInventoryManager.inventoryUI;
        GameObject inventoryUIInstance = Instantiate(remoteInventoryUI);
        inventoryUIInstance.GetComponent<RemoteInventoryUI>().remoteInventory = this;
        inventoryUIInstance.transform.SetParent(playerUI.transform, false);
        remoteInventoryManager.newUI = inventoryUIInstance;
        remoteInventoryManager.SetInventoryActive();
    }

    public IRemoteInventory GetRemoteInventoryComponent() {
        return this;
    }

    void Start() {
        inventoryCore = GetComponent<InventoryCore>();
    }
}