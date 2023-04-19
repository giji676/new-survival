using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Interactable {
    protected override void Interact(GameObject interactingObject, InteractionType interactionType) {
        base.Interact(interactingObject, interactionType);
        MeleWeapon meleWeapon = interactingObject.GetComponent<MeleWeapon>();
        if (meleWeapon) {
            if (meleWeapon.data.toolType == ToolType.Tree) {
                Mine("node");
            }
        }
    }

    public void Mine(string arg) {
        Debug.Log(arg);
    }
}