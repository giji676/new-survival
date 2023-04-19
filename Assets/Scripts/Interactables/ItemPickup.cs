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

        if (interactionType == InteractionType.Hit) return;

        PickUp(interactingObject, interactionType);
    }
    
    void PickUp(GameObject interactingObject, InteractionType interactionType) {
        PlayerInventoryManager inventoryManager = interactingObject.GetComponent<PlayerInventoryManager>();
        InventoryItem afterTransferInventoryItem = inventoryManager.AddHotbarFirst(inventoryItem);

        if (afterTransferInventoryItem.item == null) {
            Destroy(gameObject);
            return;
        }

        inventoryItem.currentStack = afterTransferInventoryItem.currentStack;
    }
}