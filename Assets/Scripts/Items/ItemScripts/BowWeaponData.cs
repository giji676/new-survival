using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName="Inventory/Bow Weapon")]
public class BowWeaponData : Equipment {
    public ArrowAmmoData arrowAmmoData;
    public float reloadTime;
    public float minVelocity;
    public float maxVelocity;
    public float maxPullTime;
    public LayerMask mask;
    public ToolType toolType;
}
