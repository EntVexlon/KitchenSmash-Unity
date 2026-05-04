using UnityEngine;

public class CameraMode : MonoBehaviour
{
    [SerializeField] private CameraModeType Mode;

    private void Update()
    {
        switch (Mode)
        {
            case CameraModeType.LookAtCamera:
                Vector3 target_pos = Camera.main.transform.position;
                target_pos.x = transform.position.x; 
                transform.LookAt(target_pos);
                break;
            case CameraModeType.LookForward:  
                //I will add Later maybe
                break;
        }
    }
}

public enum CameraModeType
{
    LookAtCamera,
    LookForward
}
