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

    protected override void Interact(GameObject interactingObject) {
        base.Interact(interactingObject);

        PickUp(interactingObject);
    }
    
    void PickUp(GameObject interactingObject) {
        if (TryHotbarPickUp(interactingObject).item == null) {
            Destroy(gameObject);
        }
        else {
            if (TryInventoryPickUp(interactingObject).item == null) {
                Destroy(gameObject);
            }
        }
    }
    
    InventoryItem TryInventoryPickUp(GameObject interactingObject) {
        Inventory inventory = interactingObject.GetComponent<Inventory>();
        return inventory.Add(inventoryItem);
    }

    InventoryItem TryHotbarPickUp(GameObject interactingObject) {
        Hotbar hotbar = interactingObject.GetComponent<Hotbar>();
        return hotbar.Add(inventoryItem);
    }}
