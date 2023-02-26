using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticWeapon : HandHeld {
    public RangedWeaponData data;
    private bool isFiring = false;
    private bool fire = false;
    private int currentSpareAmmo;
    private int currentMangizeAmmo;
    
    private Camera cam;
    
    void Start() {
        playerInputs.movementActions.Fire.performed += ctx => FirePerform();
        playerInputs.movementActions.Fire.canceled += ctx => FireCancel();
        playerInputs.movementActions.Reload.performed += ctx => Reload();
        currentMangizeAmmo = data.maxMagazineCapacity;
        currentSpareAmmo = data.maxTotalSpareAmmo;
        
        cam = GetComponentInParent<PlayerMotor>().cam;
    }

    void Update() {
        if (fire && !isFiring) {
            Fire();
        }
    }

    void FirePerform() {
        fire = true;
    }

    void FireCancel() {
        fire = false;
    }

    void Fire() {
        if (currentMangizeAmmo > 0) {
            StartCoroutine(FireBullet());
        }
        else if (currentSpareAmmo > 0) {
            Reload();
        }
    }

    IEnumerator FireBullet() {
        isFiring = true;
        
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 999, Color.red);
        RaycastHit hitInfo; 
        
        if (Physics.Raycast(ray, out hitInfo, 999, data.mask)) {
            if (hitInfo.collider.GetComponent<Interactable>() != null) {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                if (interactable.gameObject.tag == "Enemy") {
                    Dummy hitObject = hitInfo.collider.gameObject.GetComponent<Dummy>();
                    hitObject.TakeDamage(data.damage);
                }
            }
        }
        currentMangizeAmmo --;
        yield return new WaitForSeconds(data.fireRate);
        isFiring = false;
    }

    void Reload() {
        if (currentMangizeAmmo == data.maxMagazineCapacity) {
            return;
        }
        if (currentSpareAmmo == 0) {
            return;
        }
        int emptyMagazineAmmo = data.maxMagazineCapacity - currentMangizeAmmo;

        if (currentSpareAmmo >= emptyMagazineAmmo) {
            currentMangizeAmmo += emptyMagazineAmmo;
            currentSpareAmmo -= emptyMagazineAmmo;
        }
        else {
            currentMangizeAmmo += currentSpareAmmo;
            currentSpareAmmo -= currentSpareAmmo;
        }
    }
}