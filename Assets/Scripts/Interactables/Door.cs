using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {
    public Transform pivot;
    public Vector3 closedRot;
    public Vector3 openRot;
    public float rotationDuration = 0.5f; // Duration of rotation in seconds
    
    private Coroutine rotationCoroutine;
    
    private bool isOpen = false;
    private bool inTransit = false;
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
        if (inTransit) return;
        // if (rotationCoroutine != null) return;

        rotationCoroutine = StartCoroutine(InterpolateRotation(openRot, closedRot, rotationDuration));
        isOpen = false;
    }

    private void Open() {
        if (inTransit) return;
        // if (rotationCoroutine != null) return;

        rotationCoroutine = StartCoroutine(InterpolateRotation(closedRot, openRot, rotationDuration));
        isOpen = true;
    }

    private IEnumerator InterpolateRotation(Vector3 startRot, Vector3 endRot, float duration) {
        inTransit = true;
        Quaternion startQuaternion = Quaternion.Euler(startRot);
        Quaternion endQuaternion = Quaternion.Euler(endRot);

        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            float t = elapsedTime / duration;
            Quaternion interpolatedQuaternion = Quaternion.Slerp(startQuaternion, endQuaternion, t);
            pivot.localRotation = interpolatedQuaternion;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pivot.localRotation = endQuaternion; // Ensure final rotation is exact
        inTransit = false;
    }
}
