using UnityEngine;
using UnityEngine.AI;

public class MargotPointAndClickMovement : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask groundLayer;
    [SerializeField] private Animator animator;
    private NavMeshAgent agent;
    private Vector3 lastValidPosition;
    public float maxHeightDifference = 1f; // La différence de hauteur autorisée avant de remettre l'agent sur le NavMesh

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (mainCamera == null)
            mainCamera = Camera.main;

        lastValidPosition = transform.position; // Initialise la position valide dès le début
    }

    void Update()
    {
        // Si on clique pour déplacer
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, groundLayer))
            {
                agent.SetDestination(hit.point); // Déplace l'agent vers le point touché
            }
        }

        // Si l'agent n'est pas en mouvement, on vérifie sa position pour s'assurer qu'elle reste sur le NavMesh
        if (!agent.pathPending && !agent.hasPath)
        {
            // On fait un raycast vers le bas pour détecter la surface du NavMesh
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
            {
                // Si la hauteur de l'agent par rapport au sol dépasse la limite autorisée, on la repositionne sur le NavMesh
                if (Mathf.Abs(transform.position.y - hit.point.y) > maxHeightDifference)
                {
                    NavMeshHit navHit;
                    if (NavMesh.SamplePosition(hit.point, out navHit, 1.0f, NavMesh.AllAreas))
                    {
                        transform.position = navHit.position; // Repositionne l'agent sur le NavMesh
                    }
                }
            }
        }

        // Met à jour l'animation selon la vitesse de l'agent
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }
}
