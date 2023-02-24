using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName="Inventory/Semi Automatic Weapon")]
public class SemiAutomaticWeaponData : Equipment {
    public int damage;
    public int currentAmmo;
    public int maxAmmo;
    public int magazineCapacity;
    public float fireRate;
}
