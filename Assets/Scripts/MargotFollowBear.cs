using UnityEngine;
using UnityEngine.AI;

public class MargotFollowBear : MonoBehaviour
{
    [Tooltip("The script to enable (e.g., CameraFollow)")]
    private MonoBehaviour scriptToActivate;

    [Tooltip("Only activate when this tag enters")]
    public string triggeringTag = "Player";
    public GameObject objectForScript;
   
    
    void Start()
    {
        scriptToActivate = objectForScript.GetComponent<BearWalk_Jihye>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggeringTag) && scriptToActivate != null)
        {
            scriptToActivate.enabled = true;
        
        }
    }
}
