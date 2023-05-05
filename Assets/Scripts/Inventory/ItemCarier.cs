using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCarier : MonoBehaviour {
    public Item item;

    private Image image;
    private CraftingManager craftingManager;

    private void Start() {
        craftingManager = GetComponentInParent<CraftingManager>();
        image = GetComponent<Image>();
        image.sprite = item.icon;
    }

    public void Craft() {
        craftingManager.TryCraftItem(item);
    }
}