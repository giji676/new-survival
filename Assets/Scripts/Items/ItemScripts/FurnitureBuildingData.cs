using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName="Inventory/Furniture Building")]
public class FurnitureBuildingData : Equipment
{
    public int health;
    public GameObject buildingPrefab;
    public GameObject itemPrefab;
    public LayerMask colliderLayerMask;
}