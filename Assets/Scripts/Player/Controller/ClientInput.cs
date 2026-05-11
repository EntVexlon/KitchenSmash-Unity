using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClientInput : MonoBehaviour
{
    public static ClientInput Instance { get; private set; }
    public PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractExecute;
    public event EventHandler OnGamePause;
    private const string UserKeyBind = "UserKeyBind";

    public enum BindKey
    {
        MoveForward,
        MoveBackward,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAction
    }
    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();

        if(PlayerPrefs.HasKey(UserKeyBind))
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(UserKeyBind));


        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += InteractAction;
        playerInputActions.Player.InteractExecute.performed += InteractExecute;
        playerInputActions.Player.MainEscape.performed += BackButtonAction;

    }

    private void BackButtonAction(InputAction.CallbackContext context) =>
    OnGamePause?.Invoke(this, EventArgs.Empty);

    private void InteractAction(InputAction.CallbackContext context) =>
        //I Know thier is no need for a Event For this But I am a Clean Code Lover So.
        OnInteractAction?.Invoke(this, EventArgs.Empty);

    private void InteractExecute(InputAction.CallbackContext context) =>
        OnInteractExecute?.Invoke(this, EventArgs.Empty);

    public string GetBindKeyText(BindKey bind_key)
    {
        switch (bind_key)
        {
          case BindKey.MoveForward:
              return playerInputActions.Player.Move.bindings[1].ToDisplayString();
          case BindKey.MoveBackward:
              return playerInputActions.Player.Move.bindings[2].ToDisplayString();
          case BindKey.MoveLeft:
              return playerInputActions.Player.Move.bindings[3].ToDisplayString();
          case BindKey.MoveRight:
              return playerInputActions.Player.Move.bindings[4].ToDisplayString();
          case BindKey.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
          case BindKey.InteractAction:
              return playerInputActions.Player.InteractExecute.bindings[0].ToDisplayString();
            default:
                return string.Empty;
        }


    }

    public void Rebind(BindKey bind_key, Action OnReBound, Action OnBindCancel = null) { 
    playerInputActions.Player.Disable();

        InputAction input_action;
        int binding_index;

        switch(bind_key)
        {
          default:
          case BindKey.MoveForward: input_action = playerInputActions.Player.Move; binding_index = 1;break;
          case BindKey.MoveBackward: input_action = playerInputActions.Player.Move; binding_index = 2;break;
          case BindKey.MoveLeft: input_action = playerInputActions.Player.Move; binding_index = 3;break;
          case BindKey.MoveRight: input_action = playerInputActions.Player.Move; binding_index = 4;break;
          case BindKey.Interact: input_action = playerInputActions.Player.Interact; binding_index = 0;break;
          case BindKey.InteractAction: input_action = playerInputActions.Player.InteractExecute; binding_index = 0;break;
        }



        input_action.PerformInteractiveRebinding(binding_index)
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(callback => {
                callback.Dispose();
                OnReBound?.Invoke();
                playerInputActions.Player.Enable();
                PlayerPrefs.SetString(UserKeyBind, input_action.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            } )
            .OnCancel(callback =>
            {
                callback.Dispose();
                playerInputActions.Player.Enable();
                OnBindCancel?.Invoke();
            })
            .Start();
    }

}
