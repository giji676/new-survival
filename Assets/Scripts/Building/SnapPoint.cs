using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPoint : MonoBehaviour {
    public Transform point;
    public LayerMask buildingLayerMask;
    public GameObject parent;
    Vector3 size;

    void Start() {
        size = transform.lossyScale;
    }
    
    private void Update()
    {
        return;
        Collider[] colliders = Physics.OverlapBox(transform.position, size / 2, transform.rotation, buildingLayerMask);

        foreach (Collider collider in colliders) {
            if (collider.gameObject != parent) {
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }
}