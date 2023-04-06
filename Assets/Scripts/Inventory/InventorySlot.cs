using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour {
    public Image icon;
    [SerializeField] private TextMeshProUGUI stackText;
    public InventoryItem inventoryItem;
    private Inventory inventory;
    private Hotbar hotbar;

    private void Start() {
        inventory = GetComponentInParent<Inventory>();
        hotbar = GetComponentInParent<Hotbar>();
    }
    
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

    public void FromInventoryTransfer() {
        if (inventoryItem == null || inventoryItem.item == null) return;
        if (hotbar.Add(inventoryItem).item == null) {
            int index = inventory.GetItemIndex(inventoryItem);
            if (index < 0) return;
            inventory.Remove(index);
            ClearSlot();
        }
    }
    
    public void FromHotbarTransfer() {
        if (inventoryItem == null || inventoryItem.item == null) return;
        if (inventory.Add(inventoryItem).item == null) {
            int index = hotbar.GetItemIndex(inventoryItem);
            if (index < 0) return;
            hotbar.Remove(index);
            ClearSlot();
        }
    }
}
