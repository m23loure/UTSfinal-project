using UnityEngine;
using UnityEngine.AI;

public class BearWalk_Jihye : MonoBehaviour
{
    private Animator animator;
    private Transform player;

    public float followDistance = 10f;
    public float minDistance = 2f; // New: the bear will stop if closer than this
    public float moveSpeed = 1f;
    public float rotationSpeed = 5f;
    [SerializeField] GameObject julie; 
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < followDistance && distance > minDistance)
        {
            animator.SetBool("isWalking", true);
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            transform.position += transform.forward * moveSpeed * Time.deltaTime;

           
            //NavMeshAgent agent = julie.GetComponent<NavMeshAgent>();
            //agent.speed = 10f;
        }
        else
        {
            //animator.SetBool("isWalking", false);
        }
    }
}