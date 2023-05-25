using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCraftInfo : MonoBehaviour {
    public Image amountImage;
    public Image itemTypeImage;
    public Image totalImage;
    public Image haveImage;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI itemTypeText;
    public TextMeshProUGUI totalText;
    public TextMeshProUGUI haveText;
    public Color dissableColor;
    public Color enableColor;
    private PlayerInventoryManager playerInventoryManager;
    public InventoryItem inventoryItem;
    public int amount;


    private void Start() {
        playerInventoryManager = GetComponentInParent<PlayerInventoryManager>();
        Clear();
    }

    public void UpdateInfo(InventoryItem _inventoryItem, int _amount) {
        Enable();
        inventoryItem = _inventoryItem;
        amount = _amount;
        amountText.text = inventoryItem.currentStack.ToString();
        itemTypeText.text = inventoryItem.item.name;
        totalText.text = amount.ToString();
        UpdateHaveCount();

        playerInventoryManager.onItemChangedCallback += UpdateHaveCount;
    }

    private void UpdateHaveCount() {
        haveText.text = playerInventoryManager.GetItemCount(inventoryItem.item).ToString();
    }

    public void Clear() {
        Dissable();
        amountText.text = "";
        itemTypeText.text = "";
        totalText.text = "";
        haveText.text = "";
        
        inventoryItem = null;
        amount = 0;
        playerInventoryManager.onItemChangedCallback -= UpdateHaveCount;
    }

    public void Dissable() {
        amountImage.color = dissableColor;
        itemTypeImage.color = dissableColor;
        totalImage.color = dissableColor;
        haveImage.color = dissableColor;
    }

    public void Enable() {
        amountImage.color = enableColor;
        itemTypeImage.color = enableColor;
        totalImage.color = enableColor;
        haveImage.color = enableColor;
    }
}