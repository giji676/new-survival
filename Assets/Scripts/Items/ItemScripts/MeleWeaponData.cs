using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName="Inventory/Mele Weapon")]
public class MeleWeaponData : Equipment {
    public int damage;
    public float attackSpeed;
    public float attackDelay;
}
