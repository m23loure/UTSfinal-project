using UnityEngine;

public class RollingStonePhysics : MonoBehaviour
{
    public Rigidbody stoneRigidbody;
    public Transform targetPosition;
    public float forceMagnitude = 10f;
    public string triggeringTag = "Player";
    public AudioSource rockSound; 

    private bool hasRolled = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasRolled && other.CompareTag(triggeringTag))
        {
            Vector3 direction = (targetPosition.position - stoneRigidbody.position).normalized;
            stoneRigidbody.isKinematic = false;
            stoneRigidbody.AddForce(direction * forceMagnitude, ForceMode.Impulse);
            stoneRigidbody.AddTorque(Random.onUnitSphere * forceMagnitude, ForceMode.Impulse);

            hasRolled = true;
            rockSound.Play(); 
        }
    }
}
