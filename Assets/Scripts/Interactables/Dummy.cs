using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Interactable {
    [SerializeField] private int maxHealth = 100;
    private int Health { get; set; }

    public delegate void OnHealthChange();
    public OnHealthChange onHealthChangeCallback;

    private void Start() {
        Health = maxHealth;
    }

    protected override void Interact(GameObject interactingObject, InteractionType interactionType) {
        base.Interact(interactingObject, interactionType);
        Debug.Log("dummy interact");
        HandHeld handHeld = interactingObject.GetComponent<HandHeld>();
        // Debug.Log(handHeld);
        if (handHeld) TakeDamage(handHeld.damage);
    }

    public void TakeDamage(int damage) {
        Health -= damage;
        if (onHealthChangeCallback != null)
            onHealthChangeCallback.Invoke();

        // print(Health);
        if (Health <= 0) Destroy(gameObject);
    }
}