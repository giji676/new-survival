using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName="Inventory/Item")]
public class Item : ScriptableObject {
    new public string name = "New Item";
    public Sprite icon = null;
    public bool stackable = true;
    public int maxStack = 32;
    
    [Header("Crafting")]
    public bool isCraftable = false;
    public List<InventoryItem> craftingRecipe;
    public int returnAmount;
}
