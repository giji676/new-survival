using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidableAnimal : Interactable {
    [SerializeField] Transform riderMountPoint;
    private bool mounted = false;
    private GameObject rider;
    protected override void Interact(GameObject interactingObject, InteractionType interactionType) {
        base.Interact(interactingObject, interactionType);

        if (interactionType == InteractionType.Hit) {
            GetComponent<Dummy>().BaseInteract(interactingObject, interactionType);
            return;
        }
        Mount(interactingObject);
    }

    private void Update() {
        if (mounted)
            rider.transform.position = riderMountPoint.position;
    }

    private void Mount(GameObject interactingObject) {
        if (mounted) {
            mounted = false;
            GetComponent<Animal>().enabled = true;
            rider = null;
        }
        else {
            mounted = true;
            GetComponent<Animal>().enabled = false;
            rider = interactingObject;
        }
    }
}