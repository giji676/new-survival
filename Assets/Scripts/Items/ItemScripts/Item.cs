using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName="Inventory/Item")]
public class Item : ScriptableObject {
    new public string name = "New Item";
    public Sprite icon = null;
    public bool stackable = true;
    public int maxStack = 32;
}
