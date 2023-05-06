using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCraftInfoManager : MonoBehaviour {
    public List<ItemCraftInfo> itemCraftInfos;
    private CraftingManager craftingManager;
    private Item item;

    private void Start() {
        craftingManager = GetComponentInParent<CraftingManager>();
    }
    
    public void UpdateInfo(Item _item) {
        Clear();
        item = _item;
        for (int i = 0; i < item.craftingRecipe.Count; i++) {
            InventoryItem inventoryItem = item.craftingRecipe[i];
            itemCraftInfos[i].UpdateInfo(inventoryItem);
        }
    }

    public void Clear() {
        for (int i = 0; i < itemCraftInfos.Count; i++) {
            itemCraftInfos[i].Clear();
        }
    }

    public void Craft() {
        craftingManager.TryCraftItem(item);
    }
}