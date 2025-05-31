using UnityEngine;

public class MargotCameraScript : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; // Target position and rotation
    [SerializeField] private float moveDuration = 1.0f; // Duration of transition

    private bool shouldLerp = false;
    private float lerpTime = 0f;
    private Vector3 startPos;
    private Quaternion startRot;

    void Update()
    {
        if (shouldLerp)
        {
            lerpTime += Time.deltaTime;
            float t = Mathf.Clamp01(lerpTime / moveDuration);

            // Smooth interpolation
            Camera.main.transform.position = Vector3.Lerp(startPos, cameraTransform.position, t);
            Camera.main.transform.rotation = Quaternion.Slerp(startRot, cameraTransform.rotation, t);

            if (t >= 1f)
            {
                shouldLerp = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Store current camera state
            startPos = Camera.main.transform.position;
            startRot = Camera.main.transform.rotation;

            // Reset interpolation
            lerpTime = 0f;
            shouldLerp = true;
        }
    }
}
