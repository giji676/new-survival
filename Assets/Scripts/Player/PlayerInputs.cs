using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour {
    public PlayerControls playerControls;
    public PlayerControls.MovementActions movementActions;
    public PlayerControls.InteractionActions interactionActions;
    
    private PlayerMotor playerMotor;
    private Hotbar hotbar;
    
    public GameObject inventoryUI;


    void Awake() {
        playerControls = new PlayerControls();
        movementActions = playerControls.Movement;
        interactionActions = playerControls.Interaction;
    }

    void Start() {
        playerMotor = GetComponent<PlayerMotor>();
        movementActions.Jump.performed += ctx => playerMotor.Jump();
        interactionActions.Inventory.performed += ctx => InventoryTrigger();
        
        hotbar = GetComponent<Hotbar>();

        InventoryTrigger();
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
        playerMotor.ProcessLook(movementActions.Look.ReadValue<Vector2>());
    }
    
    public void InventoryTrigger() {
        // Called from InputManager
        if (inventoryUI.activeSelf) {
            inventoryUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else {
            inventoryUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
    public void InventoryTriggerFromCrate() {
        inventoryUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
