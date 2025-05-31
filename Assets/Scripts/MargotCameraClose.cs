using UnityEngine;

public class MargotCameraClose : MonoBehaviour
{
    [Tooltip("Target to follow (e.g., the player)")]
    public Transform target;

    [Tooltip("Offset from the target")]
    public Vector3 offset = new Vector3(0,0.01f, -0.01f);

    [Tooltip("Smooth speed for camera movement")]
    public float smoothSpeed = 1;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //transform.LookAt(target); // Optional: camera always looks at the player
    }
}
