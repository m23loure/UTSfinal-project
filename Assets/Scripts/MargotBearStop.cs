using UnityEngine;
using UnityEngine.AI; 
public class MargotBearStop : MonoBehaviour
{
    public Animator animator; 
    public GameObject bear; 
    public GameObject julie; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BearWalk_Jihye script = bear.GetComponent<BearWalk_Jihye>();
            if (script != null)
            {
                script.enabled = false;

                animator.SetBool("isWalking", false);
                animator.SetBool("isStopping", true);

                // Stop movement: keep current position and rotation
                bear.GetComponent<Rigidbody>()?.Sleep(); // Optional: freezes Rigidbody if there's one
                //NavMeshAgent agent = julie.GetComponent<NavMeshAgent>();
                //agent.speed = 2f;
                //Debug.Log(agent.speed); 
            }
        }
    }
}
