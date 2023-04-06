using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHeld : MonoBehaviour {
    [HideInInspector]
    public PlayerInputs playerInputs;
    public int damage;

    void Awake() {
        playerInputs = GetComponentInParent<PlayerInputs>();
    }

    public virtual void Unequip() {}
}
