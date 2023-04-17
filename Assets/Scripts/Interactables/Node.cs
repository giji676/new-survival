using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : Interactable {
    [SerializeField] private Item item;
    [SerializeField] private int baseAmount;
    [SerializeField] private GameObject stoneDropPrefab;

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
        ItemDropTest(inventoryItem, interactingObject);

        InventoryItem afterTransferInventoryItem = inventoryManager.AddItem(inventoryItem);
        if (afterTransferInventoryItem.item == null) return;

        // GameObject droppedPrefab = Instantiate(stoneDropPrefab, interactingObject.transform.position, Quaternion.identity);
        // Rigidbody rb = droppedPrefab.GetComponent<Rigidbody>();
        // droppedPrefab.GetComponent<ItemPickup>().stack = afterTransferInventoryItem.currentStack;
        // rb.AddForce(Vector3.forward * 2f, ForceMode.Impulse);
    }

    private void ItemDropTest(InventoryItem inventoryItem, GameObject interactingObject) {
        GameObject droppedPrefab = Instantiate(stoneDropPrefab, interactingObject.transform.position, Quaternion.identity);
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
