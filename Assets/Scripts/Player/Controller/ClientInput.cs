using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClientInput : MonoBehaviour
{
    public static ClientInput Instance { get; private set; }
    public PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractExecute;
    public event EventHandler OnGamePause;
    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += InteractAction;
        playerInputActions.Player.InteractExecute.performed += InteractExecute;
        playerInputActions.Player.GamePanel.performed += BackButtonAction;
    }

    private void BackButtonAction(InputAction.CallbackContext context)
    {
    OnGamePause?.Invoke(this, EventArgs.Empty);
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
