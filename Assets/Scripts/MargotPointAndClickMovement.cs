using UnityEngine;
using UnityEngine.AI;

public class MargotPointAndClickMovement : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask groundLayer;
    [SerializeField] private Animator animator; 
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, groundLayer))
            {
                agent.SetDestination(hit.point);
            }
        }
           float speed = agent.velocity.magnitude;

        
        animator.SetFloat("Speed", speed);
        
    }
}
