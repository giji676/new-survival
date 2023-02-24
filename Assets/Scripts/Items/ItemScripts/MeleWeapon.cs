using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleWeapon : HandHeld {
    public MeleWeaponData data;

    void Start() {
        playerInputs.movementActions.Fire.performed += ctx => Attack();
    }

    void Attack() {}
}
