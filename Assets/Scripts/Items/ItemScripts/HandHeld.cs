using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHeld : MonoBehaviour {
    [HideInInspector]
    public PlayerInputs playerInputs;

    void Awake() {
        playerInputs = GetComponentInParent<PlayerInputs>();
    }
}
