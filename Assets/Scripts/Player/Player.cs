using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float move_speed = 5f;
    [SerializeField] private float obj_rotation_speed = 4f;
    [SerializeField] private ClientInput client_input;
    [HideInInspector] public bool IsWalking;
    private Rigidbody rb;
    private Vector3 move;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {

        HandleMovement();
    }

    private void HandleMovement()
    {
        //Player Movement System| My Kezhap xD
        move = client_input.MovementVector();
        //transform.position += (move * move_speed) * Time.deltaTime;
        rb.MovePosition(rb.position + (move * move_speed) * Time.deltaTime);


        //For IDLE/WALK Animation 
        IsWalking = move != Vector3.zero;

        //For Object Look Point Rotation
        Transform visual_obj = transform.GetChild(0);
        if (move != Vector3.zero)
            visual_obj.forward = Vector3.Slerp(visual_obj.forward, move, obj_rotation_speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ClearCounter cc = collision.gameObject.GetComponent<ClearCounter>();
        if (cc) cc.Interact(); else return; 
    }

}
    