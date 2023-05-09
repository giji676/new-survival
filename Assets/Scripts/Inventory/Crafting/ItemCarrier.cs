using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCarrier : MonoBehaviour {
    public Item item;

    private Image image;
    private ItemCraftDisplay itemCraftDisplay;

    private void Start() {
        itemCraftDisplay = transform.root.GetComponentInChildren<ItemCraftDisplay>();
        image = GetComponent<Image>();
        image.sprite = item.icon;
    }

    public void DisplayInfo() {
        itemCraftDisplay.UpdateCraftInfo(item);
        itemCraftDisplay.UpdateItemInfo(item);
    }
}