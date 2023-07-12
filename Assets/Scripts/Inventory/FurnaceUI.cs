using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceUI : RemoteInventoryUI {
    public Furnace furnace;

    public void Toggle() {
        furnace.ToggleFire();
    }
}
