using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName="Inventory/Arrow Ammo")]
public class ArrowAmmoData : Equipment {
    public float velocity;
    public LayerMask mask;
    public ToolType toolType;
}
