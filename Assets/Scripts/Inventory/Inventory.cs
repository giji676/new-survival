using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : InventoryCore {
    void Start() {
        inventoryItems = new InventoryItem[inventorySpace];
    }
}
