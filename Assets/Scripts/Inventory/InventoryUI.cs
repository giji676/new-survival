using UnityEngine;

public class InventoryUI : MonoBehaviour {
    // Hotbar
    private InventorySlot[] hotbarSlots;
    public Transform hotbarUI;
    private Hotbar hotbar;
    
    // Inventory
    private InventorySlot[] inventorySlots;
    public Transform inventoryUI;
    private Inventory inventory;
    
    public GameObject inventoryUIReference;

    void Start() {
        hotbar = GetComponentInParent<Hotbar>();
        hotbar.onItemChangedCallback += UpdateHotbarUI;
        hotbarSlots = hotbarUI.GetComponentsInChildren<InventorySlot>();
        
        inventory = GetComponentInParent<Inventory>();
        inventory.onItemChangedCallback += UpdateInventoryUI;
        inventorySlots = inventoryUI.GetComponentsInChildren<InventorySlot>();
    }

    void UpdateHotbarUI() {
        for (int i = 0; i < hotbarSlots.Length; i++) {
            if (hotbar.inventoryItems[i] != null) {
                if (hotbar.inventoryItems[i].item != null) {
                    hotbarSlots[i].ClearSlot();
                    hotbarSlots[i].AddItem(hotbar.inventoryItems[i]);
                }
                else {
                    hotbarSlots[i].ClearSlot();
                }
            }
            else {
                hotbarSlots[i].ClearSlot();
            }
        }
    }
    
    void UpdateInventoryUI() {
        for (int i = 0; i < inventorySlots.Length; i++) {
            if (inventory.inventoryItems[i] != null) {
                if (inventory.inventoryItems[i].item != null) {
                    inventorySlots[i].ClearSlot();
                    inventorySlots[i].AddItem(inventory.inventoryItems[i]);
                }
                else {
                    inventorySlots[i].ClearSlot();
                }
            }
            else {
                inventorySlots[i].ClearSlot();
            }
    }
    }
}