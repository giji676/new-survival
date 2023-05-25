using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour {
    private PlayerInventoryManager inventoryManager;
    public List<Item> craftableItems;
    public GameObject itemCarrierPrefab;
    public GameObject craftingItemPrefab;
    public RectTransform itemListTransform;
    public RectTransform craftQueParent;
    private List<InventoryItem> craftingQue = new List<InventoryItem>();
    private List<GameObject> craftingQueItemDisplays = new List<GameObject>();
    private bool crafting = false;

    private void Start() {
        inventoryManager = GetComponent<PlayerInventoryManager>();
        UpdateCraftableItems();
    }

    private void Update() {
        QueCraft();
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

    // the item in inventoryItem represents the item to be crafted
    // the currentStack in inventoryItem represents the number to be crafted
    // if the inventory contains enough materials (inventoryItem.currentStack*recipeItem.currentstack of each material)
    // remove the materials
    // create a craftingItem image in the crafting que (UI)
    // add the inventoryItem to the crafting que
    public void TryCraftItem(InventoryItem inventoryItem) {
        if (inventoryItem == null || inventoryItem.item == null) return;
        if (!inventoryItem.item.isCraftable) return;
        if (inventoryItem.item.craftingRecipe.Count == 0) return;

        foreach (InventoryItem _inventoryItem in inventoryItem.item.craftingRecipe) {
            InventoryItem tempInventoryItem = new InventoryItem(_inventoryItem.item);
            tempInventoryItem.currentStack = inventoryItem.currentStack * _inventoryItem.currentStack;
            InventoryItem returned = inventoryManager.CheckItemExists(tempInventoryItem);
            if (returned.item == null)
                return;
        }

        foreach (InventoryItem _inventoryItem in inventoryItem.item.craftingRecipe) {
            InventoryItem tempInventoryItem = new InventoryItem(_inventoryItem.item);
            tempInventoryItem.currentStack = inventoryItem.currentStack * _inventoryItem.currentStack;
            inventoryManager.RemoveItem(tempInventoryItem);
        }

        // InventoryItem craftedItem = new InventoryItem(inventoryItem.item);
        // craftedItem.currentStack = inventoryItem.item.returnAmount;
        // inventoryManager.AddInventoryFirst(craftedItem);
        GameObject instantiatedCraftingItem = Instantiate(craftingItemPrefab);
        instantiatedCraftingItem.GetComponent<RectTransform>().SetParent(craftQueParent);

        CraftingItem craftingItem = instantiatedCraftingItem.GetComponent<CraftingItem>();
        craftingItem.UpdateData(inventoryItem);

        craftingQue.Add(inventoryItem);
        craftingQueItemDisplays.Add(instantiatedCraftingItem);
    }

    // if craftingQue isn't empty
    // and nothing is crafting at the moment
    // start crafting a single item from the amount to be crafted (currentStack)
    // decrease the amount after it has been crafted
    // update the craftingItem UI in the craftingQue
    private void QueCraft() {
        if (craftingQue.Count == 0) return;
        if (crafting) return;
        
        if (craftingQue[0].currentStack > 0) {
            StartCoroutine(Craft(craftingQue[0].item));
        }
        else {
            craftingQue.RemoveAt(0);
            Destroy(craftingQueItemDisplays[0]);
            craftingQueItemDisplays.RemoveAt(0);
        }
    }

    IEnumerator Craft(Item item) {
        crafting = true;
        yield return new WaitForSeconds(item.craftTime);
        
        InventoryItem craftedItem = new InventoryItem(item);
        craftedItem.currentStack = item.returnAmount;
        inventoryManager.AddInventoryFirst(craftedItem);

        craftingQue[0].currentStack--;
        craftingQueItemDisplays[0].GetComponent<CraftingItem>().UpdateData(craftingQue[0]);
        crafting = false;
    }
}