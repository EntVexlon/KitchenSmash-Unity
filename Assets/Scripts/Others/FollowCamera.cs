using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform Player;

    [SerializeField] private float MinZ = -10f;
    [SerializeField] private float MaxZ = 20f;

    [SerializeField] private float MinX = -10f;
    [SerializeField] private float MaxX = 10f;

    [SerializeField] private float SmoothSpeed = 5f;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - Player.position;
    }

    private void LateUpdate()
    {
        float targetZ = Mathf.Clamp(Player.position.z, MinZ, MaxZ);
        float targetX = Mathf.Clamp(Player.position.x, MinX, MaxX);

        Vector3 targetPos = new Vector3(
            targetX + offset.x,         
            transform.position.y,       
            targetZ + offset.z          
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Time.deltaTime * SmoothSpeed
        );
    }
}
