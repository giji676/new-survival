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
                hotbarSlots[i].ClearSlot();
                hotbarSlots[i].AddItem(hotbar.inventoryItems[i]);
            }
            else {
                hotbarSlots[i].ClearSlot();
            }
        }
    }
    
    void UpdateInventoryUI() {
        for (int i = 0; i < inventorySlots.Length; i++) {
            if (inventory.inventoryItems[i] != null) {
                inventorySlots[i].ClearSlot();
                inventorySlots[i].AddItem(inventory.inventoryItems[i]);
            }
            else {
                inventorySlots[i].ClearSlot();
            }
        }
    }
}