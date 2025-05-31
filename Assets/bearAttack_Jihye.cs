using UnityEngine;
using UnityEngine.AI;

public class bearAttack_Jihye : MonoBehaviour
{
    private Animator animator;
    public AudioSource roarAudio;

    [Header("Player References")]
    [Tooltip("Animator of the player")]
    public Animator playerAnimator;

    [Tooltip("NavMeshAgent of the player")]
    public NavMeshAgent playerAgent;

    [Header("Speed Settings")]
    public float newSpeed = 4f;
    private float originalSpeed;
    public GameObject Julie; 

    void Start()
    {
        animator = GetComponent<Animator>();

        if (roarAudio == null)
        {
            roarAudio = GetComponent<AudioSource>();
        }

        if (playerAgent != null)
        {
            originalSpeed = playerAgent.speed;

        }
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (animator != null)
            {
                animator.SetBool("isPlayerNear", true);
            }

            if (roarAudio != null)
            {
                roarAudio.Play();
            }
             if (playerAgent != null)
            {
            Debug.Log("Running Julie");
            playerAgent.speed = newSpeed;
            playerAnimator.SetBool("isRunning", true);
            Julie.GetComponent<AudioSource>().Play(); 
            }

          

           
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (animator != null)
            {
                animator.SetBool("isPlayerNear", false);
            }

            if (roarAudio != null)
            {
                roarAudio.Stop();
            }
            playerAnimator.SetBool("isRunning", false); 
            
        }
    }
}
