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
    private GameObject lastHoveredObject;
    private bool outline;

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
        
        outline = false;
        
        if (Physics.Raycast(ray, out hitInfo, distance, mask)) {
            // If the object hit is an Interactable
            if (hitInfo.collider.GetComponent<Interactable>() != null) {
                lastHoveredObject = hitInfo.collider.gameObject;

                interactable = hitInfo.collider.GetComponent<Interactable>();
                interactable.BaseOutline(true);
                outline = true;
                playerUI.UpdateText(interactable.promptMessage);

                if (playerInputs.interactionActions.Interact.triggered) {
                    // If the object is an interactable, and E is pressed (interact)
                    interactable.BaseInteract(gameObject, InteractionType.Access);
                }
            }
        }
        if (!outline && lastHoveredObject != null) {
            lastHoveredObject.GetComponent<Interactable>().BaseOutline(false);
            lastHoveredObject = null;
        }
    }
}

public enum InteractionType {
    Hit,
    Access,
}