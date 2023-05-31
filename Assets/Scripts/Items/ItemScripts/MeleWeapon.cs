using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MeleWeapon : HandHeld {
    public MeleWeaponData data;
    private bool isAttacking = false;
    private bool attack = false;
    
    private Camera cam;

    void Start() {
        damage = data.damage;
        playerInputs.movementActions.Fire.performed += AttackAction;
        playerInputs.movementActions.Fire.canceled += AttackAction;
        
        cam = GetComponentInParent<PlayerMotor>().cam;
    }

    public override void Unequip() {
        playerInputs.movementActions.Fire.performed -= AttackAction;
        playerInputs.movementActions.Fire.canceled -= AttackAction;
        StopAllCoroutines();
    }

    void Update() {
        if (attack && !isAttacking) {
            Attack();
        }
    }

    void AttackAction(InputAction.CallbackContext ctx) {
        attack = ctx.ReadValue<float>() == 1 ? true : false;
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
                interactable.BaseInteract(gameObject, InteractionType.Hit);
            }
        }
    }
    
    IEnumerator FullSwingCooldown() {
        isAttacking = true;
        yield return new WaitForSeconds(data.totalAttackSpeed);
        isAttacking = false;
    }
}
