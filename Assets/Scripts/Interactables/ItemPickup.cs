using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable {
    public Item item;
    public int stack;
    private InventoryItem inventoryItem;

    void Start() {
        inventoryItem = new InventoryItem(item);
        inventoryItem.currentStack = Mathf.Clamp(stack, 0, item.maxStack);
    }

    protected override void Interact(GameObject interactingObject, InteractionType interactionType) {
        base.Interact(interactingObject, interactionType);

        PickUp(interactingObject);
    }
    
    void PickUp(GameObject interactingObject) {
        PlayerInventoryManager inventoryManager = interactingObject.GetComponent<PlayerInventoryManager>();
        InventoryItem afterTransferInventoryItem = inventoryManager.AddItem(inventoryItem);

        if (afterTransferInventoryItem.item == null) {
            Destroy(gameObject);
            return;
        }

        inventoryItem.currentStack = afterTransferInventoryItem.currentStack;
    }
}