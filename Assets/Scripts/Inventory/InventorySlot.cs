using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour {
    public Image icon;
    [SerializeField] private TextMeshProUGUI stackText;
    public InventoryItem inventoryItem;
    
    public void AddItem(InventoryItem newInventoryItem) {
        inventoryItem = newInventoryItem;
        icon.sprite = inventoryItem.item.icon;
        icon.enabled = true;

        if (inventoryItem.currentStack > 1)
            stackText.text = inventoryItem.currentStack.ToString();
        else
            stackText.text = "";
    }

    public void ClearSlot() {
        inventoryItem = null;
        icon.sprite = null;
        icon.enabled = false;
        stackText.text = "";
    }
}
