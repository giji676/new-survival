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

    public void TryCraftItem(Item item) {
        if (!item.isCraftable) return;
        if (item.craftingRecipe.Count == 0) return;

        foreach (InventoryItem inventoryItem in item.craftingRecipe) {
            InventoryItem returned = inventoryManager.CheckItemExists(inventoryItem);
            if (returned.item == null)
                return;
        }

        foreach (InventoryItem inventoryItem in item.craftingRecipe) {
            inventoryManager.RemoveItem(inventoryItem);
        }

        InventoryItem craftedItem = new InventoryItem(item);
        craftedItem.currentStack = item.returnAmount;
        inventoryManager.AddInventoryFirst(craftedItem);
    }
}