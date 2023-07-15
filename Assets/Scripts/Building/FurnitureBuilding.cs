using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureBuilding : MonoBehaviour {
    public FurnitureBuildingData buildingData;
    public float rayDistance = 5f;
    float curDistance;
    private Camera cam;
    
    private void Start() {
        cam = GetComponentInParent<PlayerMotor>().cam;
    }

    private void Update() {
        Build();
    }

    private void Build() {
        curDistance = rayDistance;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, buildingData.colliderLayerMask)) {
            transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
            transform.position = hit.point;
        }
        else {
            transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
        }

        if (Input.GetMouseButtonDown(0)) {
            Instantiate(buildingData.buildingPrefab, transform.position, transform.rotation);
            Hotbar hotbar = GetComponentInParent<Hotbar>();
            hotbar.UnequipAtActiveIndex();
            Destroy(gameObject);
        }
    }
}