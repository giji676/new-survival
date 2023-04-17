using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : Interactable {
    [SerializeField] private Item item;
    [SerializeField] private int baseAmount;

    protected override void Interact(GameObject interactingObject) {
        base.Interact(interactingObject);
        MeleWeapon meleWeapon = interactingObject.GetComponent<MeleWeapon>();

        if (!meleWeapon) return;
        if (meleWeapon.data.toolType != ToolType.Node) return;

        Mine(interactingObject, false);
    }

    public void Mine(GameObject interactingObject, bool fromHitTarget) {
        PlayerInventoryManager inventoryManager = interactingObject.GetComponentInParent<PlayerInventoryManager>();
        
        InventoryItem inventoryItem = MakeItem(fromHitTarget);

        InventoryItem afterTransferInventoryItem = inventoryManager.AddItem(inventoryItem);
        if (afterTransferInventoryItem.item == null) return;

        Debug.Log("dropped" + afterTransferInventoryItem);
        // Drop afterTransferInventoryItem on ground
    }

    private InventoryItem MakeItem(bool fromHitTarget) {
        InventoryItem inventoryItem = new InventoryItem(item);
        inventoryItem.currentStack = fromHitTarget ? (int)(baseAmount * 1.5) : baseAmount;
        return inventoryItem;
    }
}
