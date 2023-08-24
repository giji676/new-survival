using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName="Inventory/Arrow Ammo")]
public class ArrowAmmoData : Equipment {
    public float velocity;
    public float velocityMultiplier = 1;
    public float angularVelocityMultiplier = 1;
    public LayerMask mask;
    public ToolType toolType;
}
