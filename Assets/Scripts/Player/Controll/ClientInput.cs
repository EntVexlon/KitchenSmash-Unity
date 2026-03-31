using UnityEngine;

public class ClientInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }


    public Vector3 MovementVector()
    {
        Vector3 inputVectorValue = playerInputActions.Player.Move.ReadValue<Vector3>();
        return inputVectorValue;
    }
}
