using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCraftInfo : MonoBehaviour {
    public Image amountImage;
    public Image itemTypeImage;
    public Image totalImage;
    public Image haveImage;
    public TextMeshProUGUI amount;
    public TextMeshProUGUI itemType;
    public TextMeshProUGUI total;
    public TextMeshProUGUI have;
    public Color dissableColor;
    public Color enableColor;


    private void Start() {
        Clear();
    }

    public void UpdateInfo(InventoryItem inventoryItem, int _amount) {
        Enable();
        amount.text = inventoryItem.currentStack.ToString();
        itemType.text = inventoryItem.item.name;
        total.text = _amount.ToString();
    }

    public void Clear() {
        Dissable();
        amount.text = "";
        itemType.text = "";
        total.text = "";
        have.text = "";
    }

    public void Dissable() {
        amountImage.color = dissableColor;
        itemTypeImage.color = dissableColor;
        totalImage.color = dissableColor;
        haveImage.color = dissableColor;
    }

    public void Enable() {
        amountImage.color = enableColor;
        itemTypeImage.color = enableColor;
        totalImage.color = enableColor;
        haveImage.color = enableColor;
    }
}