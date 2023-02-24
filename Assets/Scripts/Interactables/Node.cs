using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : Interactable {
    protected override void Interact(GameObject interactingObject) {
        base.Interact(interactingObject);
        Debug.Log("mined");
    }
}
