using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutomaticWeapon : HandHeld {
    public SemiAutomaticWeaponData data;
    
    void Start() {
        playerInputs.movementActions.Fire.performed += ctx => Fire();
    }

    void Fire() {}
}
