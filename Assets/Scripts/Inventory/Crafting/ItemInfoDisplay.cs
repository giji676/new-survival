using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfoDisplay : MonoBehaviour {
    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI craftTime;
    public TextMeshProUGUI returnAmount;
    public TextMeshProUGUI description;

    private void Start() {
        Clear();
    }

    public void UpdateInfo(Item item) {
        icon.enabled = true;
        icon.sprite = item.icon;
        itemName.text = item.name;
        craftTime.text = item.craftTime.ToString();
        returnAmount.text = item.returnAmount.ToString();
        description.text = item.description;
    }

    public void Clear() {
        icon.sprite = null;
        icon.enabled = false;
        itemName.text = "";
        craftTime.text = "";
        returnAmount.text = "";
        description.text = "";
    }
}