using System;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private LayerMask CounterLayer;
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private ClientInput Client_Input;
    [SerializeField] private PlayerMoveHandler PlayerMove;
    [SerializeField] private Player PlayerHandler;
    [HideInInspector] public ContainerCounter LastContainerCounter;
    public RaycastHit Hit;
    private CounterVisualHandler CurrentCounter;
    private CounterVisualHandler PreviousCounter;
    private bool IsHitCounter;
    private float InteractObjectDistance = 2f;

    private void Start()
    {
        Client_Input.OnInteract += InteractCounter;
        Client_Input.OnInteractAction += InteractExecute;
    }
    private void Update()
    {

        IsHitCounter = Physics.Raycast(PlayerTransform.position, PlayerMove.LastMoveDir, out Hit,
        InteractObjectDistance, CounterLayer);



        //Counter Indication When Pointing Close At Counter 
        if (IsHitCounter)
        {
            CurrentCounter = Hit.collider.GetComponentInChildren<CounterVisualHandler>();
            if (PreviousCounter != null && PreviousCounter != CurrentCounter)
                PreviousCounter.HideIndicator();

            CurrentCounter.SetIndicator();
            PreviousCounter = CurrentCounter;
        }
        else if (PreviousCounter != null)
        {
            PreviousCounter.HideIndicator();
            PreviousCounter = null;
            CurrentCounter = null;
        }


}


    //Counter Interact
    public void InteractCounter(object sender, EventArgs e)
    {
        if(GameHandler.Instance.CurrentState is GameHandler.GameState.Countdown)
            return;
        if(IsHitCounter)
            if(Hit.collider.TryGetComponent(out ICounter counter))
                PlayerHandler.TryInteractCounter(counter);
    }

    public void InteractExecute(object sender, EventArgs e)
    {
        if (GameHandler.Instance.CurrentState is GameHandler.GameState.Countdown)
            return;
        if (IsHitCounter)
            if (Hit.collider.TryGetComponent(out ICounter counter))
                PlayerHandler.TryInteractExecute(counter);
    }

    private void OnDestroy()
    {
        Client_Input.OnInteract -= InteractCounter;
        Client_Input.OnInteractAction -= InteractExecute;
    }
}
