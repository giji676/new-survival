using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : Interactable {
    [SerializeField] private Item item;
    [SerializeField] private int baseAmount;
    [SerializeField] private GameObject dropPrefab;

    protected override void Interact(GameObject interactingObject, InteractionType interactionType) {
        base.Interact(interactingObject, interactionType);
        MeleWeapon meleWeapon = interactingObject.GetComponent<MeleWeapon>();

        if (!meleWeapon) return;
        if (meleWeapon.data.toolType != ToolType.Node) return;
        if (interactionType == InteractionType.Access) return;

        Mine(interactingObject, false);
    }

    public void Mine(GameObject interactingObject, bool fromHitTarget) {
        PlayerInventoryManager inventoryManager = interactingObject.GetComponentInParent<PlayerInventoryManager>();
        
        InventoryItem inventoryItem = MakeItem(fromHitTarget);

        InventoryItem afterTransferInventoryItem = inventoryManager.AddInventoryFirst(inventoryItem);
        if (afterTransferInventoryItem.item == null) return;

        ItemDrop(afterTransferInventoryItem, interactingObject);
    }

    private void ItemDrop(InventoryItem inventoryItem, GameObject interactingObject) {
        GameObject droppedPrefab = Instantiate(dropPrefab, interactingObject.transform.position, Quaternion.identity);
        Rigidbody rb = droppedPrefab.GetComponent<Rigidbody>();
        droppedPrefab.GetComponent<ItemPickup>().stack = inventoryItem.currentStack;
        rb.AddForce(Vector3.forward * 2f, ForceMode.Impulse);
    }

    private InventoryItem MakeItem(bool fromHitTarget) {
        InventoryItem inventoryItem = new InventoryItem(item);
        inventoryItem.currentStack = fromHitTarget ? (int)(baseAmount * 1.5) : baseAmount;
        return inventoryItem;
    }
}
