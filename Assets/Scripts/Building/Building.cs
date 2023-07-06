using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    public BuildingData buildingData;
    public float rayDistance = 5f;
    float curDistance;
    private Camera cam;
    private Collider targetCollider;

    
    private void Start() {
        cam = GetComponentInParent<PlayerMotor>().cam;
    }

    private void Update() {
        Build();
    }

    private void Build() {
        curDistance = rayDistance;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        bool newRay = true; // If the ray hits a different snapping collider, continue the ray from the hit point with the remaining distance
        while (newRay) {
            if (Physics.Raycast(ray, out hit, curDistance, buildingData.colliderLayerMask)) {
                targetCollider = hit.collider;
                if (!targetCollider.GetComponent<SnapPoint>()) return; // If it doesn't have a snap point return
                SnapPoint snapPoint = targetCollider.GetComponent<SnapPoint>();
                if (snapPoint.buildingTag != buildingData.targetTag) { // If it is a different snapping collider
                    curDistance = rayDistance - hit.distance; // Get the remaining distance
                    ray = new Ray(hit.point + cam.transform.forward * 0.1f, cam.transform.forward); // Set the ray to the new ray
                    
                    continue;
                }
                targetCollider = hit.collider;

                transform.position = snapPoint.point.position;
                transform.rotation = Quaternion.Euler(0, snapPoint.point.eulerAngles.y, 0);
            }
            else if (Physics.Raycast(ray, out hit, rayDistance)) {
                transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
                transform.position = hit.point;
                targetCollider = null;
            }
            else {
                transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
            }
            newRay = false;
        }

        if (Input.GetMouseButtonDown(0)) {
            if (targetCollider == null && !buildingData.isFoundation) return;

            Instantiate(buildingData.buildingPrefab, transform.position, transform.rotation);
            Hotbar hotbar = GetComponentInParent<Hotbar>();
            hotbar.UnequipAtActiveIndex();
            Destroy(gameObject);
        }
    }
}