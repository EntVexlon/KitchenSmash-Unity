using UnityEngine;

public class PlayerMoveHandler : MonoBehaviour
{
    [SerializeField] private ClientInput clientInput;
    [SerializeField] private float move_speed = 5f;
    [HideInInspector] public Vector3 moveDir;
    [HideInInspector] public Vector3 LastMoveDir;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //Player Movement System| My Kezhap xD
        moveDir = clientInput.playerInputActions.Player.Move.ReadValue<Vector3>();
        //transform.position += (move * move_speed) * Time.deltaTime;
        rb.MovePosition(rb.position + (moveDir * move_speed) * Time.deltaTime);

        //Last Move
        if (moveDir != Vector3.zero)
        {
            GetComponent<FootstepEmitter>().TryPlayAudio(transform);
            LastMoveDir = moveDir;
        }



    }
}       
