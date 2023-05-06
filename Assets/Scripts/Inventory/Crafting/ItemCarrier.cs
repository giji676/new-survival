using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCarrier : MonoBehaviour {
    public Item item;

    private Image image;
    private ItemInfoDisplay itemInfoDisplay;
    private ItemCraftInfoManager itemCraftInfoManager;

    private void Start() {
        itemInfoDisplay = transform.root.GetComponentInChildren<ItemInfoDisplay>();
        itemCraftInfoManager = transform.root.GetComponentInChildren<ItemCraftInfoManager>();
        image = GetComponent<Image>();
        image.sprite = item.icon;
    }

    public void DisplayInfo() {
        itemInfoDisplay.UpdateInfo(item);
        itemCraftInfoManager.UpdateInfo(item);
    }
}