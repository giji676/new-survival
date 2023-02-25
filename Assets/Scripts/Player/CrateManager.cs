using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateManager : MonoBehaviour {
    public bool crateAccessed = false;
    public GameObject inventoryUI;
    public GameObject newUI;

    public void SetInventoryActive() {
        inventoryUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SetInventoryUnactive() {
        crateAccessed = false;
        Destroy(newUI);
    }
}