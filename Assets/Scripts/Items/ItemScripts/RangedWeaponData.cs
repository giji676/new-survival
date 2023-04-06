using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName="Inventory/Ranged Weapon")]
public class RangedWeaponData : Equipment {
    public int damage;
    public int maxTotalSpareAmmo;
    public int maxMagazineCapacity;
    public float fireRate;
    public float reloadTime;
    public LayerMask mask;
    public ToolType toolType;
}
