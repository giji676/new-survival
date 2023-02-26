using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Interactable {
    int health = 1000;

    protected override void Interact(GameObject interactingObject) {
        base.Interact(interactingObject);
    }

    public void TakeDamage(int damage) {
        health -= damage;
        print(health);
    }
}