using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidableAnimal : Interactable {
    [SerializeField] Transform riderMountPoint;
    protected override void Interact(GameObject interactingObject, InteractionType interactionType) {
        base.Interact(interactingObject, interactionType);

        Debug.Log("access");
        if (interactionType == InteractionType.Hit) {
            GetComponent<Dummy>().BaseInteract(interactingObject, interactionType);
            return;
        }
        Mount(interactingObject);
    }

    private void Start() {

    }

    private void Mount(GameObject interactingObject) {
        Debug.Log("mount");
        GetComponent<Animal>().enabled = false;
        interactingObject.transform.position = riderMountPoint.position;
    }
}