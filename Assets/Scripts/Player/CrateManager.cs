using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateManager : MonoBehaviour {
    public bool crateAccessed = false;
    public GameObject inventoryUI;
    public GameObject newUI;
    public Crate crate;

    public void SetInventoryActive() {
        inventoryUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SetInventoryUnactive() {
        crateAccessed = false;
        crate = null;
        newUI.GetComponent<CrateUI>().Unsubscribe();
        Destroy(newUI);
        newUI = null;
    }
}