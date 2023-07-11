using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRemoteInventory {
    GameObject remoteInventoryUI { get; set; }
    InventoryCore inventoryCore { get; set; }

    void Access(GameObject interactingObject, InteractionType interactionType);
    IRemoteInventory GetRemoteInventoryComponent();
}