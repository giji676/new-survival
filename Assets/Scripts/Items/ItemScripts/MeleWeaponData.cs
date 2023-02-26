using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName="Inventory/Mele Weapon")]
public class MeleWeaponData : Equipment {
    public int damage;
    public float attackSpeed; // How long it takes for the raycast to be cast (the peak of the attack animation)
    public float totalAttackSpeed; // How long it takes for the whole attack animation to happen (the peak, and moving the weapon back to the normal position) // MUST be longer than attackSpeed
    public float range;
    public LayerMask mask;
}
