using System;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private LayerMask CounterLayer;
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private ClientInput Client_Input;
    [SerializeField] private PlayerMoveHandler PlayerMove;
    [SerializeField] private Player PlayerHandler;
    [HideInInspector] public RaycastHit Hit;
    [HideInInspector] public ContainerCounter LastContainerCounter;
    private CounterVisualHandler CounterIndicate;
    private bool IsHitCounter;
    private float InteractObjectDistance = 2f;

    private void Start()
    {
        Client_Input.OnInteractAction += InteractCounter;
        Client_Input.OnInteractExecute += InteractExecute;
    }
    private void Update()
    {
        //For Testing
        UnityEngine.Debug.DrawRay(PlayerTransform.position, PlayerMove.LastMoveDir *
            InteractObjectDistance, Color.red);
        //
        //if (pmh.LastMoveDir == Vector3.zero) return;
        //

        IsHitCounter = Physics.Raycast(PlayerTransform.position, PlayerMove.LastMoveDir, out Hit,
        InteractObjectDistance, CounterLayer);



        //Counter Indication When Pointing Close At Counter 
        if (IsHitCounter)
        {
            CounterIndicate =
                Hit.collider.GetComponentInChildren<CounterVisualHandler>();
            CounterIndicate.SetIndicator();
        }
        else if (CounterIndicate != null)
            CounterIndicate.HideIndicator();
    }


    //Counter Interact
    public void InteractCounter(object sender, EventArgs e)
    {
        if(IsHitCounter)
            if(Hit.collider.TryGetComponent(out IinteractCounter counter))
                PlayerHandler.TryInteractCounter(counter);
    }

    public void InteractExecute(object sender, EventArgs e)
    {
        if (IsHitCounter)
            if (Hit.collider.TryGetComponent(out IinteractCounter counter))
                PlayerHandler.TryInteractExecute(counter);
    }
}
