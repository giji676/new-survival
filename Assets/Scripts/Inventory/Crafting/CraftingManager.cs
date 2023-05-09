using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour {
    private PlayerInventoryManager inventoryManager;
    public List<Item> craftableItems;
    public GameObject itemCarrierPrefab;
    public RectTransform itemListTransform;

    private void Start() {
        inventoryManager = GetComponent<PlayerInventoryManager>();
        UpdateCraftableItems();
    }

    public void AddCraftableItem(Item item) {
        craftableItems.Add(item);
        UpdateCraftableItems();
    }

    private void UpdateCraftableItems() {
        foreach (Item item in craftableItems) {
            GameObject instantiatedCarrier = Instantiate(itemCarrierPrefab);
            instantiatedCarrier.GetComponent<RectTransform>().SetParent(itemListTransform);
            instantiatedCarrier.GetComponent<ItemCarrier>().item = item;
        }
    }

    public void TryCraftItem(InventoryItem inventoryItem) {
        if (inventoryItem == null || inventoryItem.item == null) return;
        if (!inventoryItem.item.isCraftable) return;
        if (inventoryItem.item.craftingRecipe.Count == 0) return;

        foreach (InventoryItem _inventoryItem in inventoryItem.item.craftingRecipe) {
            InventoryItem returned = inventoryManager.CheckItemExists(_inventoryItem);
            if (returned.item == null)
                return;
        }

        foreach (InventoryItem _inventoryItem in inventoryItem.item.craftingRecipe) {
            inventoryManager.RemoveItem(_inventoryItem);
        }

        InventoryItem craftedItem = new InventoryItem(inventoryItem.item);
        craftedItem.currentStack = inventoryItem.item.returnAmount;
        inventoryManager.AddInventoryFirst(craftedItem);
    }
}