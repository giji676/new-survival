using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    public void Shoot(float velocity, Vector3 direction) {
        gameObject.transform.SetParent(null);
        rb.useGravity = true;
        rb.AddForce(direction * velocity, ForceMode.Impulse);
    }
}