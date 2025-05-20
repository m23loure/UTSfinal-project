using UnityEngine;

public class BearWalk_Jihye : MonoBehaviour
{
    private Animator animator;
    private Transform player;

    public float followDistance = 10f;
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < followDistance)
        {
        
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            animator.SetBool("isWalking", true);
        }
        else
        {
         
            animator.SetBool("isWalking", false);
        }
    }
}
