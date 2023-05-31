using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Interactable {
    [SerializeField] private int health = 100;

    protected override void Interact(GameObject interactingObject, InteractionType interactionType) {
        base.Interact(interactingObject, interactionType);
        HandHeld handHeld = interactingObject.GetComponent<HandHeld>();
        Debug.Log(handHeld);
        if (handHeld) TakeDamage(handHeld.damage);
    }

    public void TakeDamage(int damage) {
        health -= damage;
        print(health);
        if (health <= 0) Destroy(gameObject);
    }
}