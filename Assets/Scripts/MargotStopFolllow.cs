using UnityEngine;

public class MargotStopFollow : MonoBehaviour
{
    [Tooltip("The script to enable (e.g., CameraFollow)")]
    private MonoBehaviour scriptToActivate;

    [Tooltip("Only activate when this tag enters")]
    public string triggeringTag = "Player";

    void Start()
    {
        scriptToActivate = Camera.main.GetComponent<MargotCameraClose>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggeringTag) && scriptToActivate != null)
        {
            scriptToActivate.enabled = false;
            
        }
    }
}
