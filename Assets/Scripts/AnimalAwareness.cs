using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAwareness : MonoBehaviour {
    [SerializeField] private Animal animal;

    private void OnTriggerEnter(Collider other) {
        // Make sure the Player instance has a tag "Player"
        // Needs to be attached to a GO thats a child of the Animal class
        if (!other.CompareTag("Player")) return;
        animal.PlayerTrigger(other);
    }
}