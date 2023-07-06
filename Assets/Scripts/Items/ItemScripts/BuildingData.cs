using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName="Inventory/Building")]
public class BuildingData : Equipment
{
    public int health;
    public bool isFoundation;
    public LayerMask colliderLayerMask; // Layer maksed used by Building script to determine if the building should snap
    public BuildingTag targetTag;
    public GameObject buildingPrefab;
    public GameObject itemPrefab;
}