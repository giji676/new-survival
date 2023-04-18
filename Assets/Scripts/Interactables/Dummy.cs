using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Interactable {
    int health = 1000;

    protected override void Interact(GameObject interactingObject, InteractionType interactionType) {
        base.Interact(interactingObject, interactionType);
        HandHeld handHeld = interactingObject.GetComponent<HandHeld>();
        if (handHeld) TakeDamage(handHeld.damage);
    }

    public void TakeDamage(int damage) {
        health -= damage;
        print(health);
    }
}