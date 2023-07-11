using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteInventoryManager : MonoBehaviour {
    public bool remoteInventoryAccessed = false;
    public GameObject inventoryUI;
    public GameObject newUI;
    public IRemoteInventory remoteInventory;

    public void SetInventoryActive() {
        inventoryUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SetInventoryUnactive() {
        remoteInventoryAccessed = false;
        remoteInventory = null;
        newUI.GetComponent<RemoteInventoryUI>().Unsubscribe();
        Destroy(newUI);
        newUI = null;
    }
}