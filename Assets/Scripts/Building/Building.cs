using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    public BuildingData buildingData;
    public float rayDistance = 5f;
    private Camera cam;
    private Collider targetCollider;

    
    private void Start() {
        cam = GetComponentInParent<PlayerMotor>().cam;
    }

    private void Update() {
        Build();
    }

    private void Build() {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance, buildingData.colliderLayerMask)) {
            targetCollider = hit.collider;
            if (!targetCollider.GetComponent<SnapPoint>()) return;
            SnapPoint snapPoint = targetCollider.GetComponent<SnapPoint>();
            if (snapPoint.buildingTag != buildingData.targetTag) return;
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

        if (Input.GetMouseButtonDown(0)) {
            if (targetCollider == null && !buildingData.isFoundation) return;

            Instantiate(buildingData.buildingPrefab, transform.position, transform.rotation);
            Hotbar hotbar = GetComponentInParent<Hotbar>();
            hotbar.UnequipAtActiveIndex();
            Destroy(gameObject);
        }
    }
}