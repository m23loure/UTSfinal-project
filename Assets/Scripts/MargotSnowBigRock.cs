using UnityEngine;

public class MargotSnowBigRock : MonoBehaviour
{
    public GameObject snowParticlePrefab;
    public float impactThreshold = 2f;
    public float particleOffsetY = 0.3f;
    public float particleOffsetX = 0.2f; // New: X offset for lateral placement

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > impactThreshold)
        {
            if (collision.collider.CompareTag("Ground"))
            {
                ContactPoint contact = collision.contacts[0];
                Vector3 spawnPoint = contact.point 
                                     + Vector3.up * particleOffsetY 
                                     + transform.right * particleOffsetX;

                GameObject snow = Instantiate(snowParticlePrefab, spawnPoint, Quaternion.identity);

                float scaleFactor = transform.localScale.magnitude * 0.1f;

                ParticleSystem ps = snow.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    var main = ps.main;
                    main.startSize = new ParticleSystem.MinMaxCurve(scaleFactor * 0.8f, scaleFactor * 1.5f);
                    main.startLifetime = 1.5f;
                    main.startSpeed = new ParticleSystem.MinMaxCurve(0.8f, 1.5f);
                    main.gravityModifier = 0.1f;

                    var emission = ps.emission;
                    emission.rateOverTime = 500;

                    var burst = new ParticleSystem.Burst(0f, 50, 80); // Stronger burst
                    emission.SetBursts(new ParticleSystem.Burst[] { burst });

                    var shape = ps.shape;
                    shape.angle = 20;
                    shape.radius = scaleFactor * 0.3f;
                    
                    ps.Play();
                }

                Destroy(snow, 4f);
            }
        }
    }
}
