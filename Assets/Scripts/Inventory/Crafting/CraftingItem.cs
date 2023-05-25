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

    public void UpdateData(InventoryItem _inventoryItem) {
        inventoryItem = _inventoryItem;
        icon.sprite = inventoryItem.item.icon;
        amountText.text = inventoryItem.currentStack.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cancelBtn.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cancelBtn.enabled = false;
    }
}