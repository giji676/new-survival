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
    private RemoteInventoryManager remoteInventoryManager;

    private void Start() {
        inventory = GetComponentInParent<Inventory>();
        hotbar = GetComponentInParent<Hotbar>();
        inventoryManager = GetComponentInParent<PlayerInventoryManager>();
        remoteInventoryManager = GetComponentInParent<RemoteInventoryManager>();
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
        
        if (remoteInventoryManager.remoteInventory != null) {
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

        if (remoteInventoryManager.remoteInventory != null) {
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
        IRemoteInventory remoteInventory = remoteInventoryManager.remoteInventory.GetRemoteInventoryComponent();
        return remoteInventory.inventoryCore.Add(inventoryItem);
    }

    public void FromCrateTransfer() {
        if (inventoryItem == null || inventoryItem.item == null) return;
        if (!remoteInventoryManager.remoteInventoryAccessed || remoteInventoryManager.remoteInventory == null) return;

        int index = remoteInventoryManager.remoteInventory.inventoryCore.GetItemIndex(inventoryItem);
        InventoryItem afterTransferInventoryItem = inventoryManager.AddInventoryFirst(remoteInventoryManager.remoteInventory.inventoryCore.inventoryItems[index]);
        if (afterTransferInventoryItem.item == null) {
            remoteInventoryManager.remoteInventory.inventoryCore.Remove(index);
            ClearSlot();
            return;
        }

        remoteInventoryManager.remoteInventory.inventoryCore.UpdateItem(afterTransferInventoryItem, index);
    }
}
