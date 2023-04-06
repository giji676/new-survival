using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName="Inventory/Building")]
public class BuildingData : Equipment
{
    public int health;
    public bool isFoundation;
    public LayerMask colliderLayerMask;
    public LayerMask snapPointsLayerMask;
    public GameObject buildingPrefab;
    public GameObject itemPrefab;
}