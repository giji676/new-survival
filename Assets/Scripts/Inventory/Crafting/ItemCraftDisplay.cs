using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCraftDisplay : MonoBehaviour {
    private Item item;
    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI craftTime;
    public TextMeshProUGUI returnAmount;
    public TextMeshProUGUI description;
    public List<ItemCraftInfo> itemCraftInfos;
    private CraftingManager craftingManager;
    public TMP_InputField craftInput;
    private int craftAmount = 1;

    private void Start() {
        craftingManager = GetComponentInParent<CraftingManager>();
        craftInput.text = craftAmount.ToString();
        craftInput.onValueChanged.AddListener(delegate  {ValueChangeCheck(); });
        ClearItemInfo();
    }

    public void ValueChangeCheck() {
        if (craftInput.text == "-" || craftInput.text == "") {
            craftAmount = 1;
            craftInput.text = craftAmount.ToString();
            return;
        }
        craftAmount = Convert.ToInt32(craftInput.text);
        if (craftAmount <= 0) {
            craftAmount = 1;
            craftInput.text = craftAmount.ToString();
        }
        else if (craftAmount > 999) {
            craftAmount = 1;
            craftInput.text = craftAmount.ToString();
        }
        UpdateCraftInfo(item);
    }

    public void UpdateItemInfo(Item _item) {
        icon.enabled = true;
        icon.sprite = _item.icon;
        itemName.text = _item.name;
        craftTime.text = _item.craftTime.ToString();
        returnAmount.text = _item.returnAmount.ToString();
        description.text = _item.description;
    }

    public void ClearItemInfo() {
        icon.sprite = null;
        icon.enabled = false;
        itemName.text = "";
        craftTime.text = "";
        returnAmount.text = "";
        description.text = "";
    }
    
    public void UpdateCraftInfo(Item _item) {
        ClearCraftInfo();
        item = _item;
        for (int i = 0; i < item.craftingRecipe.Count; i++) {
            InventoryItem inventoryItem = item.craftingRecipe[i];
            int amount = inventoryItem.currentStack * craftAmount;
            itemCraftInfos[i].UpdateInfo(inventoryItem, amount);
        }
    }
    
    public void ClearCraftInfo() {
        for (int i = 0; i < itemCraftInfos.Count; i++) {
            itemCraftInfos[i].Clear();
        }
    }

    public void Craft() {
        InventoryItem inventoryItem = new InventoryItem(item);
        inventoryItem.currentStack = craftAmount;
        craftingManager.TryCraftItem(inventoryItem);
    }

    public void Increase() {
        if (craftAmount ==  999) return;
        craftAmount++;
        craftInput.text = craftAmount.ToString();
        UpdateCraftInfo(item);
    }

    public void Decrease() {
        if (craftAmount ==  1) return;
        craftAmount--;
        craftInput.text = craftAmount.ToString();
        UpdateCraftInfo(item);
    }
}