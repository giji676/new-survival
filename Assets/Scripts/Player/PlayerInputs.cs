using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour {
    public InputActions inputActions;
    public InputActions.MovementActions movementActions;
    public InputActions.InteractionActions interactionActions;
    
    private PlayerMotor playerMotor;
    private Hotbar hotbar;
    
    public GameObject inventoryUI;
    public GameObject craftMenuUI;

    void Awake() {
        inputActions = new InputActions();
        movementActions = inputActions.Movement;
        interactionActions = inputActions.Interaction;
    }

    void Start() {
        playerMotor = GetComponent<PlayerMotor>();
        movementActions.Jump.performed += ctx => playerMotor.Jump();
        interactionActions.Inventory.performed += ctx => InventoryTrigger();
        interactionActions.CraftMenu.performed += ctx => CraftMenuTrigger();
        
        hotbar = GetComponent<Hotbar>();

        SetInventoryUnactive();
        SetCraftMenuUnactive();
    }

    void Update() {
        #region Hotbar slots

        if (interactionActions.HB0.triggered)
            hotbar.UseSlot(0);

        if (interactionActions.HB1.triggered)
            hotbar.UseSlot(1);

        if (interactionActions.HB2.triggered)
            hotbar.UseSlot(2);

        if (interactionActions.HB3.triggered)
            hotbar.UseSlot(3);

        if (interactionActions.HB4.triggered)
            hotbar.UseSlot(4);

        if (interactionActions.HB5.triggered)
            hotbar.UseSlot(5);

        #endregion
    }

    void OnEnable() {
        movementActions.Enable();
        interactionActions.Enable();
    }

    void OnDisable() {
        movementActions.Disable();
        interactionActions.Enable();
    }
   
    void FixedUpdate() {
        playerMotor.ProcessMove(movementActions.Move.ReadValue<Vector2>());
    }

    void LateUpdate() {
        if (!inventoryUI.activeSelf && !craftMenuUI.activeSelf)
            playerMotor.ProcessLook(movementActions.Look.ReadValue<Vector2>());
    }
    
    public void InventoryTrigger() {
        if (inventoryUI.activeSelf) {
            SetInventoryUnactive();
            CrateManager crateManager = GetComponent<CrateManager>();
            if (crateManager.crateAccessed) {
                crateManager.SetInventoryUnactive();
            }
        }
        else {
            SetCraftMenuUnactive();
            SetInventoryActive();
        }
    }
    
    public void SetInventoryActive() {
        inventoryUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SetInventoryUnactive() {
        inventoryUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void CraftMenuTrigger() {
        if (craftMenuUI.activeSelf) {
            SetCraftMenuUnactive();
            CrateManager crateManager = GetComponent<CrateManager>();
            if (crateManager.crateAccessed) {
                crateManager.SetInventoryUnactive();
            }
        }
        else {
            SetInventoryUnactive();
            SetCraftMenuActive();
        }
    }
    
    public void SetCraftMenuActive() {
        craftMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SetCraftMenuUnactive() {
        craftMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
