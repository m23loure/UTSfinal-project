using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class CheckpointSceneSwitcher : MonoBehaviour
{
    [Tooltip("Exact name of the scene to load when Julie reaches this checkpoint.")]
    public string sceneToLoad = "MargotScene";

    private void Awake()
    {
        // Make sure this collider is a trigger
        var col = GetComponent<Collider>();
        if (!col.isTrigger)
        {
            Debug.LogWarning($"{name}: Collider is not set to 'Is Trigger' – setting it now.", this);
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{Time.time:F2}s: {name} triggered by '{other.name}' (tag={other.tag})", this);

        // Make sure we're catching the right object
        if (!other.CompareTag("Player"))
        {
            Debug.Log($"<{name}> ignored trigger from non-Player.", this);
            return;
        }

        // Double-check the scene exists in Build Settings
        bool found = false;
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            if (path.Contains($"{sceneToLoad}.unity"))
            {
                found = true;
                break;
            }
        }

        if (!found)
        {
            Debug.LogError($"Scene '{sceneToLoad}' not found in Build Settings! Make sure you've added it.", this);
            return;
        }

        Debug.Log($"Loading scene '{sceneToLoad}'...", this);
        SceneManager.LoadScene(sceneToLoad);
    }
}
