using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CraftingItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public InventoryItem inventoryItem;
    public Image icon;
    public TextMeshProUGUI amountText;
    public Image cancelBtn;
    public CraftingManager craftingManager;

    public void UpdateData(InventoryItem _inventoryItem) {
        inventoryItem = _inventoryItem;
        icon.sprite = inventoryItem.item.icon;
        amountText.text = "x" + inventoryItem.currentStack.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        cancelBtn.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        cancelBtn.enabled = false;
    }

    public void Cancel() {
        craftingManager.Cancel(inventoryItem);
    }
}