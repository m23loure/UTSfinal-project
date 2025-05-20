using UnityEngine;

public class bearAttack_Jihye : MonoBehaviour
{
    private Animator animator;
    public AudioSource roarAudio;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (roarAudio == null)
        {
            roarAudio = GetComponent<AudioSource>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isPlayerNear", true);
            roarAudio.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isPlayerNear", false);
        }
    }

    void Update()
    {
      
    }
}
