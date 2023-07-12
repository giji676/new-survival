using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : Interactable, IRemoteInventory {
    [SerializeField] private GameObject _remoteInventoryUI;
    [SerializeField] private InventoryCore _inventoryCore;
    [SerializeField] private Item fuel;
    private bool fireOn = false;
    private bool burning = false;

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
        inventoryUIInstance.GetComponent<FurnaceUI>().remoteInventory = this;
        inventoryUIInstance.GetComponent<FurnaceUI>().furnace = this;
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

    private void Update() {
        if (fireOn && !burning) {
            StartCoroutine(BurnFuel(fuel));
        }
        if (fireOn) {
        }
    }

    public void ToggleFire() {
        if (fireOn) {
            fireOn = false;
            burning = false;
            StopAllCoroutines();
        }
        else {
            fireOn = true;
            Smelt();
        }
    }

    private void Smelt() {
        if (inventoryCore.GetOnlyItemIndex(new InventoryItem(fuel)) == -1) return;

        fireOn = true;
        foreach (InventoryItem inventoryItem in inventoryCore.inventoryItems) {
            if (inventoryItem == null || inventoryItem.item == null) continue;
            if (!inventoryItem.item.isSmeltable) continue;

            StartCoroutine(Smelt(inventoryItem));
        }
    }

    private IEnumerator Smelt(InventoryItem inventoryItem) {
        yield return new WaitForSeconds(inventoryItem.item.smeltTime);
        int index = inventoryCore.GetOnlyItemIndex(inventoryItem);
        
        inventoryItem.currentStack--;

        inventoryCore.UpdateItem(inventoryItem, index);
        InventoryItem smeltedItem = new InventoryItem(inventoryItem.item.smeltedItem.item);
        inventoryCore.Add(smeltedItem);

        if (inventoryItem.currentStack > 0) {
            StartCoroutine(Smelt(inventoryItem));
        }
    }

    private IEnumerator BurnFuel(Item item) {
        if (!item.isFuel) yield return null;
        burning = true;
        InventoryItem inventoryItem = new InventoryItem(item);
        int index = inventoryCore.GetOnlyItemIndex(inventoryItem);
        if (index != -1) {
            inventoryItem = inventoryCore.inventoryItems[index];
            inventoryItem.currentStack--;

            inventoryCore.UpdateItem(inventoryItem, index);
        }
        yield return new WaitForSeconds(item.burnTime);
        burning = false;
    }
}