using UnityEngine;

public class TriggerBearWalk : MonoBehaviour
{
    public BearWalk_Jihye bearWalkScript;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bearWalkScript.enabled = true; 
        }
    }
}
