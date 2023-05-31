using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class AutomaticWeapon : HandHeld {
    public RangedWeaponData data;
    private bool isFiring = false;
    private bool fire = false;
    private int currentSpareAmmo;
    private int currentMangizeAmmo;
    
    private Camera cam;
    
    void Start() {
        damage = data.damage;
        playerInputs.movementActions.Fire.performed += FireAction;
        playerInputs.movementActions.Fire.canceled += FireAction;
        playerInputs.movementActions.Reload.performed += ReloadAction;
        currentMangizeAmmo = data.maxMagazineCapacity;
        currentSpareAmmo = data.maxTotalSpareAmmo;
        
        cam = GetComponentInParent<PlayerMotor>().cam;
    }

    public override void Unequip() {
        playerInputs.movementActions.Fire.performed -= FireAction;
        playerInputs.movementActions.Fire.canceled -= FireAction;
        playerInputs.movementActions.Reload.performed -= ReloadAction;
        StopAllCoroutines();
    }

    void Update() {
        if (fire && !isFiring) {
            Fire();
        }
    }

    void FireAction(InputAction.CallbackContext ctx) {
        fire = ctx.ReadValue<float>() == 1;
    }

    void ReloadAction(InputAction.CallbackContext ctx) {
        Reload();
    }

    void Fire() {
        if (currentMangizeAmmo > 0) {
            StartCoroutine(FireBullet());
        }
        else if (currentSpareAmmo > 0) {
            StartCoroutine(Reload());
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
                interactable.BaseInteract(gameObject, InteractionType.Hit);
            }
        }
        currentMangizeAmmo --;
        yield return new WaitForSeconds(data.fireRate);
        isFiring = false;
    }

    IEnumerator Reload() {
        yield return new WaitForSeconds(data.reloadTime);
        if (currentMangizeAmmo == data.maxMagazineCapacity) {
            yield break;
        }
        if (currentSpareAmmo == 0) {
            yield break;
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