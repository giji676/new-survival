using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleWeapon : HandHeld {
    public MeleWeaponData data;

    private bool isAtacking = false;
    private bool attack = false;
    
    private Camera cam;

    void Start() {
        playerInputs.movementActions.Fire.performed += ctx => AttackPerform();
        playerInputs.movementActions.Fire.canceled += ctx => AttackCancel();
        
        cam = GetComponentInParent<PlayerMotor>().cam;
    }

    void Update() {
        if (attack && !isAtacking) {
            Attack();
        }
    }

    void AttackPerform() {
        attack = true;
    }

    void AttackCancel() {
        attack = false;
    }

    void Attack() {
        StartCoroutine(Swing());
        StartCoroutine(FullSwingCooldown());
    }

    IEnumerator Swing() {
        yield return new WaitForSeconds(data.attackSpeed);
        
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * data.range);
        RaycastHit hitInfo; 
        
        if (Physics.Raycast(ray, out hitInfo, data.range, data.mask)) {
            if (hitInfo.collider.GetComponent<Interactable>() != null) {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                if (interactable.gameObject.tag == "Enemy") {
                    Dummy hitObject = hitInfo.collider.gameObject.GetComponent<Dummy>();
                    hitObject.TakeDamage(data.damage);
                }
            }
        }
    }
    
    IEnumerator FullSwingCooldown() {
        isAtacking = true;
        yield return new WaitForSeconds(data.totalAttackSpeed);
        isAtacking = false;
    }
}
