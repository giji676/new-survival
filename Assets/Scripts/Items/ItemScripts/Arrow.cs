using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    public ArrowAmmoData data;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Shoot(float velocity, Vector3 direction) {
        gameObject.transform.SetParent(null);
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.velocity = transform.forward * (velocity + data.velocity);
    }

    private void FixedUpdate() {
        Vector3 cross = Vector3.Cross(transform.forward, rb.velocity.normalized);

        rb.AddTorque(cross * rb.velocity.magnitude * data.velocityMultiplier);
        rb.AddTorque((-rb.angularVelocity + Vector3.Project(rb.angularVelocity, transform.forward)) * rb.velocity.magnitude * data.angularVelocityMultiplier);
    }
}