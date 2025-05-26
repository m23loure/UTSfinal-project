using UnityEngine;

public class MargotSnowTrigger : MonoBehaviour
{
    public GameObject snowParticlePrefab;
    public float impactThreshold = 2f;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the rock hits the ground with enough force
        if (collision.relativeVelocity.magnitude > impactThreshold)
        {
            // Optional: Check the tag of what was hit
            if (collision.collider.CompareTag("Ground"))
            {
                // Spawn the snow effect at the point of impact
                ContactPoint contact = collision.contacts[0];
                Instantiate(snowParticlePrefab, contact.point, Quaternion.identity);
            }
        }
    }
}
