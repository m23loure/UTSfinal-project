using UnityEngine;
using UnityEngine.SceneManagement;

public class MargotChangeScene : MonoBehaviour
{
    [Tooltip("Name of the scene to load on trigger")]
    public string sceneToLoad;

    [Tooltip("Only trigger with this tag")]
    public string triggeringTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the correct tag
        if (other.CompareTag(triggeringTag))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
