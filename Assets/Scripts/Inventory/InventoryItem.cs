using System;

[Serializable]
public class InventoryItem {
    public Item item;
    public int currentStack = 0;

    public InventoryItem(Item item_) {
        item = item_;
        currentStack = 1;
    }
}
