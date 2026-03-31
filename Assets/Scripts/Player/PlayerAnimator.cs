using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player pc;
    private Animator anim;
    private void Start()
    {  anim = GetComponent<Animator>();}

    private void Update()
    {
        //Idk Why i Did This But I Prefer Logic and Visual Seperate xD 
        anim.SetBool("IsWalking", pc.IsWalking);
    }

}
