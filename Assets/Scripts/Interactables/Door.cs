using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {
    private bool isOpen = false;
    protected override void Interact(GameObject interactingObject, InteractionType interactionType) {
        base.Interact(interactingObject, interactionType);

        if (interactionType != InteractionType.Access) return;

        Toggle();
    }

    private void Toggle() {
        if (isOpen) Close();
        else Open();
    }

    private void Close() {
        isOpen = false;
    }

    private void Open() {
        isOpen = true;
    }
}
