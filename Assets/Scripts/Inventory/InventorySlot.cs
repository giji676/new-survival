using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour {
    public Image icon;
    [SerializeField] private TextMeshProUGUI stackText;
    public InventoryItem inventoryItem;
    private Inventory inventory;
    private Hotbar hotbar;
    private PlayerInventoryManager inventoryManager;
    private CrateManager crateManager;

    private void Start() {
        inventory = GetComponentInParent<Inventory>();
        hotbar = GetComponentInParent<Hotbar>();
        inventoryManager = GetComponentInParent<PlayerInventoryManager>();
        crateManager = GetComponentInParent<CrateManager>();
    }
    
    public void AddItem(InventoryItem newInventoryItem) {
        inventoryItem = newInventoryItem;
        icon.enabled = true;
        icon.sprite = inventoryItem.item.icon;

        if (inventoryItem.currentStack > 1)
            stackText.text = inventoryItem.currentStack.ToString();
        else
            stackText.text = "";
    }

    public void ClearSlot() {
        inventoryItem = null;
        stackText.text = "";
        
        icon.sprite = null;
        icon.enabled = false;
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
        
        int index = -1;
        if (afterTransferInventoryItem.item == null) {
            index = inventory.GetItemIndex(inventoryItem);
            inventory.Remove(index);
            ClearSlot();
            return;
        }

        index = inventory.GetItemIndex(inventoryItem);
        inventoryItem = afterTransferInventoryItem;
        inventory.UpdateItem(inventoryItem, index);
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

        int index = -1;
        if (afterTransferInventoryItem.item == null) {
            index = hotbar.GetItemIndex(inventoryItem);
            hotbar.Remove(index);
            ClearSlot();
            return;
        }

        index = hotbar.GetItemIndex(inventoryItem);
        inventoryItem = afterTransferInventoryItem;
        hotbar.UpdateItem(inventoryItem, index);
    }

    private InventoryItem ToCrateTransfer() {
        Crate crate = crateManager.crate.GetComponent<Crate>();
        return crate.Add(inventoryItem);
    }

    public void FromCrateTransfer() {
        if (inventoryItem == null || inventoryItem.item == null) return;
        if (!crateManager.crateAccessed || crateManager.crate == null) return;

        int index = crateManager.crate.GetItemIndex(inventoryItem);
        InventoryItem afterTransferInventoryItem = inventoryManager.AddInventoryFirst(crateManager.crate.inventoryItems[index]);
        if (afterTransferInventoryItem.item == null) {
            crateManager.crate.Remove(index);
            ClearSlot();
            return;
        }

        crateManager.crate.UpdateItem(afterTransferInventoryItem, index);
    }
}
