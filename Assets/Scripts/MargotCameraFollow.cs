using UnityEngine;

public class MargotCameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public Vector3 offset;    // The offset from the player (position of the camera relative to the player)
    public float smoothSpeed = 0.125f;  // Smoothness of the camera movement

    void LateUpdate()
    {
        

        // Optional: Make the camera look at the player (if desired)
        transform.LookAt(player);
    }
}
