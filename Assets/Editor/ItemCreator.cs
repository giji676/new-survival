using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class ItemCreator : EditorWindow
{
    private GameObject originalPrefab;
    private Item item;
    private MonoScript scriptToAttach;
    private LayerMask interactableLayer;
    private LayerMask FPSLayer;
    private string folderName = "GeneratedPrefabs";

    [MenuItem("Custom Tools/Create Prefabs")]
    private static void ShowWindow()
    {
        EditorWindow.GetWindow<ItemCreator>("Item Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Prefab Creation Settings", EditorStyles.boldLabel);

        originalPrefab = EditorGUILayout.ObjectField("Prefab", originalPrefab, typeof(GameObject), false) as GameObject;
        item = EditorGUILayout.ObjectField("Item", item, typeof(Item), false) as Item;
        scriptToAttach = EditorGUILayout.ObjectField("Script to Attach", scriptToAttach, typeof(MonoScript), false) as MonoScript;
        interactableLayer = EditorGUILayout.Popup("Interactable Layer", interactableLayer, GetLayerNames());
        FPSLayer = EditorGUILayout.Popup("FPS Layer", FPSLayer, GetLayerNames());

        if (GUILayout.Button("Create Prefabs"))
        {
            CreatePrefabs();
        }
    }

    private string[] GetLayerNames()
    {
        string[] layerNames = new string[32];
        for (int i = 0; i < 32; i++)
        {
            layerNames[i] = LayerMask.LayerToName(i);
        }
        return layerNames;
    }
    private void CreatePrefabs()
    {
        folderName = item.name;
        string itemsFolderPath = "Assets/Items";
        string folderPath = itemsFolderPath + "/" + folderName;

        if (!AssetDatabase.IsValidFolder(itemsFolderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Items");
        }

        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder(itemsFolderPath, folderName);
        }

        if (originalPrefab != null)
        {
            GameObject itemPickupPrefab = PrefabUtility.InstantiatePrefab(originalPrefab) as GameObject;
            CreateItemPickup(itemPickupPrefab);
            SavePrefab(itemPickupPrefab, folderPath, $"{item.name}_itempickup");
            
            GameObject heldItemPrefab = PrefabUtility.InstantiatePrefab(originalPrefab) as GameObject;
            CreateHeldItem(heldItemPrefab);
            SavePrefab(heldItemPrefab, folderPath, $"{item.name}");
        }

        AssetDatabase.Refresh();
    }

    private void CreateItemPickup(GameObject targetPrefab) {
        ItemPickup itemPickup = targetPrefab.AddComponent<ItemPickup>();
        itemPickup.promptMessage = $"[E] {item.name}";
        itemPickup.item = item;
        itemPickup.stack = 1;

        Outline outline = targetPrefab.AddComponent<Outline>();
        outline.enabled = false;

        targetPrefab.layer = interactableLayer;
    }

    private void CreateHeldItem(GameObject targetPrefab) {
        if (scriptToAttach != null)
        {
            MonoScript scriptInstance = Instantiate(scriptToAttach) as MonoScript;
            targetPrefab.AddComponent(scriptInstance.GetClass());
            targetPrefab.layer = FPSLayer;
        }
    }

    private void SavePrefab(GameObject prefab, string folderPath, string prefabName)
    {
        string prefabPath = folderPath + "/" + prefabName + ".prefab";
        PrefabUtility.SaveAsPrefabAsset(prefab, prefabPath);
        DestroyImmediate(prefab);
    }
}
