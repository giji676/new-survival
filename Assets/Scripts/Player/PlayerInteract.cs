using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
    public Camera cam;
    [SerializeField] private float distance = 10f;
    [SerializeField] private LayerMask mask;
    private PlayerUI playerUI;
    private PlayerInputs playerInputs;
    private Interactable interactable;

    void Start() {
        playerUI = GetComponent<PlayerUI>();
        playerInputs = GetComponent<PlayerInputs>();
    }

    void Update() {
        playerUI.UpdateText(string.Empty);
        PlayerInteractRay();
    }

    void PlayerInteractRay() {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, distance, mask)) {
            // If the object hit is an Interactable
            if (hitInfo.collider.GetComponent<Interactable>() != null) {
                interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);

                if (playerInputs.interactionActions.Interact.triggered) {
                    // If the object is an interactable, and E is pressed (interact)
                    interactable.BaseInteract(gameObject, InteractionType.Access);
                }
            }
        }
    }
}

public enum InteractionType {
    Hit,
    Access,
}