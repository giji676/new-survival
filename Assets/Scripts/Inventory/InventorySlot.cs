using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour {
    public Image icon;
    [SerializeField] private TextMeshProUGUI stackText;
    public InventoryItem inventoryItem;
    private Inventory inventory;
    private Hotbar hotbar;
    private CrateManager crateManager;

    private void Start() {
        inventory = GetComponentInParent<Inventory>();
        hotbar = GetComponentInParent<Hotbar>();
        crateManager = GetComponentInParent<CrateManager>();
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

        InventoryItem afterTransferInventoryItem = new InventoryItem(null);
        
        if (crateManager.crate) {
            afterTransferInventoryItem = ToCrateTransfer();
        }
        else {
            afterTransferInventoryItem = hotbar.Add(inventoryItem);
        }
        
        if (afterTransferInventoryItem.item == null) {
            int index = inventory.GetItemIndex(inventoryItem);
            inventory.Remove(index);
            ClearSlot();
        }
    }
    
    public void FromHotbarTransfer() {
        if (inventoryItem == null || inventoryItem.item == null) return;
        
        InventoryItem afterTransferInventoryItem = new InventoryItem(null);

        if (crateManager.crate) {
            afterTransferInventoryItem = ToCrateTransfer();
        }
        else {
            afterTransferInventoryItem = inventory.Add(inventoryItem);
        }

        if (afterTransferInventoryItem.item == null) {
            int index = hotbar.GetItemIndex(inventoryItem);
            hotbar.Remove(index);
            ClearSlot();
        }
    }

    private InventoryItem ToCrateTransfer() {
        Crate crate = crateManager.crate.GetComponent<Crate>();
        return crate.Add(inventoryItem);
    }
}
