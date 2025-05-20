using UnityEngine;
using UnityEngine.Playables; // Nécessaire pour accéder au PlayableDirector

public class MargotBearTrigger : MonoBehaviour
{
    public PlayableDirector director; // Référence au PlayableDirector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            director.Play(); // Lance la Timeline
        }
    }
}
