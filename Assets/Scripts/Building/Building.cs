using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
    public BuildingData buildingData;
    private bool isPlaced;
    private bool buildingBlocked;
    private Collider targetCollider;
    private Camera cam;
    
    public LayerMask passLayerMask;
    public float rayDistance = 5f;

    // NEW *********
    private Rigidbody rb;

    void Start() {
        cam = GetComponentInParent<PlayerMotor>().cam;
	    // NEW *********
	    rb = GetComponent<Rigidbody>();
    }

    void Update() {
        Build();
    }

    // NEW *********
    void OnTriggerEnter(Collider collider) {
        if (((1 << collider.gameObject.layer) & buildingData.snapPointsLayerMask.value) != 0) {
            Debug.Log(collider.gameObject.layer + " " + buildingData.snapPointsLayerMask.value);
            buildingBlocked = true;
        }
	// Maybe check layer if it's wall and floor
	// Don't allow build
	// Because it is colliding with building colliders, which means it's too close to main Building object
    }

    void OnTriggerExit(Collider collider) {
        if (((1 << collider.gameObject.layer) & buildingData.snapPointsLayerMask.value) != 0) {
            buildingBlocked = false;
        }
    }

    public void Build() {
        if (!isPlaced) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance, buildingData.colliderLayerMask)) { // If the ray hits the correct layer for the snapping
                targetCollider = hit.collider;
                if (targetCollider.gameObject == gameObject) return; // If the ray hit its own gameObject
                if (targetCollider.GetComponent<SnapPoint>() == null) return; // If the collider the ray hits doesn't have SnapPoint script

                SnapPoint snapPoint = targetCollider.GetComponent<SnapPoint>();

                // Snapping
                transform.rotation = Quaternion.Euler(0, snapPoint.point.eulerAngles.y, 0);
                // transform.eulerAngles = snapPoint.point.eulerAngles;
                transform.position = snapPoint.point.position;
            }
            else if (Physics.Raycast(ray, out hit, rayDistance, passLayerMask))
            { // If the ray hits anything other than the colliderLayerMask
                // ---------------- Colliding with the preview collider, ? add extra layerMask
                // if (hit.collider.gameObject == gameObject) {
                //     Physics.Raycast(ray, out hit, rayDistance-hit.distance);
                // }
                transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
                transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                targetCollider = null;
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            // If the building that's being placed isn't a foundation, it should not be placed on its own. It must be snapped to other buildings.
            if (targetCollider == null && !buildingData.isFoundation) return; // If there is nothing to snap to (no other building), and the gameObject isn't foundation
            if (buildingBlocked) return;
            isPlaced = true;
            // BuildingManager.isBuilding = false;
            Instantiate(buildingData.buildingPrefab, transform.position, transform.rotation);
            if (targetCollider != null) {
                // targetCollider.enabled = false;
            }
            Hotbar hotbar = GetComponentInParent<Hotbar>();
            hotbar.UnequipAtActiveIndex();
            Destroy(gameObject);
        }
    }
}