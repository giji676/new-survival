using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class SemiAutomaticWeapon : HandHeld {
    public RangedWeaponData data;
    private bool isFiring = false;
    private int currentSpareAmmo;
    private int currentMangizeAmmo;

    private Camera cam;
    
    void Start() {
        damage = data.damage;
        playerInputs.movementActions.Fire.performed += Fire;
        playerInputs.movementActions.Reload.performed += ReloadAction;
        currentMangizeAmmo = data.maxMagazineCapacity;
        currentSpareAmmo = data.maxTotalSpareAmmo;
        
        cam = GetComponentInParent<PlayerMotor>().cam; //UnityEngine.InputSystem.InputAction.CallbackContext
    }

    public override void Unequip() {
        playerInputs.movementActions.Fire.performed -= Fire;
        playerInputs.movementActions.Reload.performed -= ReloadAction;
        StopAllCoroutines();
    }

    void ReloadAction(InputAction.CallbackContext ctx) {
        Reload();
    }

    void Fire(InputAction.CallbackContext ctx) {
        if (currentMangizeAmmo > 0) {
            if (!isFiring) {
                StartCoroutine(FireBullet());
            }
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
                interactable.BaseInteract(gameObject, InteractionType.Hit);
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
