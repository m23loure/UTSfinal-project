using UnityEngine;
using UnityEngine.AI;

public class JulieEscape_Jihye : MonoBehaviour
{
    public Transform bear;
    public float safeDistance = 5f;
    public float escapeDistance = 10f;

    private NavMeshAgent agent;
    private bool isEscaping = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 6f;
        agent.acceleration = 10f;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, bear.position);

        if (distance < safeDistance && !isEscaping)
        {
            
            Vector3 escapeDirection = (transform.position - bear.position).normalized;
            Vector3 escapeTarget = transform.position + escapeDirection * escapeDistance;

            agent.SetDestination(escapeTarget);
            isEscaping = true;
        }

        
        if (isEscaping && distance > escapeDistance)
        {
            agent.ResetPath();
            isEscaping = false;
        }
    }
}
