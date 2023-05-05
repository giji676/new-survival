using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCarier : MonoBehaviour {
    public Item item;

    private Image image;
    private CraftingManager craftingManager;
    private ItemInfoDisplay itemInfoDisplay;

    private void Start() {
        craftingManager = GetComponentInParent<CraftingManager>();
        itemInfoDisplay = transform.root.GetComponentInChildren<ItemInfoDisplay>();
        image = GetComponent<Image>();
        image.sprite = item.icon;
    }

    public void Craft() {
        craftingManager.TryCraftItem(item);
        itemInfoDisplay.UpdateInfo(item);
    }
}