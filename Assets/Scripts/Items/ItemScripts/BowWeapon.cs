using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Runtime.CompilerServices;

public class BowWeapon : HandHeld {
    [SerializeField] private Transform arrowParent;
    public BowWeaponData data;
    private bool isLoading = false;
    private bool isPulling = false;
    private float pullingTime = 0f;
    private float currentVelocity = 0f;
    private GameObject arrow;
    private Camera cam;

    private void Start() {
        playerInputs.movementActions.Fire.performed += Pull;
        playerInputs.movementActions.Fire.canceled += Release;
        cam = GetComponentInParent<PlayerMotor>().cam; //UnityEngine.InputSystem.InputAction.CallbackContext
    }

    private void Update() {
        if (isPulling && pullingTime < data.maxPullTime) {
            pullingTime += Time.deltaTime;
            currentVelocity = remap(pullingTime, 0f, data.maxPullTime, data.minVelocity, data.maxVelocity);
        }
    }

    private void Pull(InputAction.CallbackContext ctx) {
        if (isPulling) return;
        isPulling = true;
        arrow = Instantiate(data.arrowAmmoData.prefab);
        arrow.transform.SetParent(arrowParent);
        arrow.transform.position = arrowParent.position;
        arrow.transform.rotation = arrowParent.rotation;
    }
    
    private void Release(InputAction.CallbackContext ctx) {
        isPulling = false;
        Debug.Log(pullingTime);
        Debug.Log(currentVelocity);
        arrow.GetComponent<Arrow>().Shoot(currentVelocity, cam.transform.forward);
        pullingTime = 0f;
        currentVelocity = 0f;
    }

    private float remap(float x, float in_min, float in_max, float out_min, float out_max) {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}