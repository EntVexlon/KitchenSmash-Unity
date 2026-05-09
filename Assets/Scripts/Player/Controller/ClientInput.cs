using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClientInput : MonoBehaviour
{
    public PlayerInputActions playerInputActions;
    [HideInInspector] public event EventHandler OnInteractAction;
    [HideInInspector] public event EventHandler OnInteractExecute;
    //Thier is No Needed To use HideInInspector But Why Not
    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += InteractAction;
        playerInputActions.Player.InteractExecute.performed += InteractExecute;
    }

    private void InteractAction(InputAction.CallbackContext context)
    {
        //I Know thier is no need for a Event For this But I am a Clean Code Lover So.
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractExecute(InputAction.CallbackContext context)
    {
        OnInteractExecute?.Invoke(this, EventArgs.Empty);
    }
}
