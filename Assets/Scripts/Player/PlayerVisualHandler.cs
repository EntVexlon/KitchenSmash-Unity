using UnityEngine;

public class PlayerVisualHandler : MonoBehaviour
{

    [SerializeField] private Transform visual_obj;
    [SerializeField] private PlayerMoveHandler pmh;
    [SerializeField] private float objRotationSpeed = 4f;
    private Animator anim;
    private bool IsWalking;
    private void Start()
    {anim = GetComponent<Animator>();}

    private void Update()
    {
        //For Idle/Walk Animation 
        IsWalking = pmh.moveDir != Vector3.zero;
        anim.SetBool("IsWalking", IsWalking);

        //For Object Look Point Rotation
        visual_obj = transform.GetChild(0);
        if (pmh.moveDir != Vector3.zero)
            visual_obj.forward = Vector3.Slerp(visual_obj.forward, pmh.moveDir,
            objRotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Yeah As You Can See i Puted the ItemHold Object in Head Object As Child
    /// First of All i still understanding 3D Moddeling Like Now The Player Object
    /// Head Is Correctly Pointing to the Direction Where Pointing , But Not Understand 
    /// Why and How So , it not like i not done any epxeriment i done experiments 
    /// To understand the Logic But Still Not Find ,But i Will Find!!
    /// </summary>


}
