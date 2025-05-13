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
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isPlayerNear", false);
        }
    }

    public void PlayRoarSound()
    {
        if (!roarAudio.isPlaying)
        {
            roarAudio.Play();
        }
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Bear_Attack2") && !roarAudio.isPlaying)
        {
            PlayRoarSound();
        }
    }
}
