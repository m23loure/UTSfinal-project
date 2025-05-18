using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MargotForestAppear : MonoBehaviour
{
    public GameObject targetObject; // L'objet à faire apparaître
    public float fadeDuration = 2f;
    public string triggeringTag = "Player";
    public AudioSource audioSource; 
    public AudioSource oldAudio; 

    private List<Material> targetMaterials = new List<Material>();
    private bool hasFadedIn = false;

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("Target object not assigned.");
            return;
        }

        Renderer[] renderers = targetObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {
            foreach (Material mat in rend.materials)
            {
                SetMaterialTransparent(mat);
                Color c = mat.color;
                c.a = 0f;
                mat.color = c;
                targetMaterials.Add(mat);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasFadedIn && other.CompareTag(triggeringTag))
        {
            StartCoroutine(FadeIn());
            audioSource.Play(); 
            oldAudio.Stop(); 
           
        }
    }

    IEnumerator FadeIn()
    {
        hasFadedIn = true;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            foreach (Material mat in targetMaterials)
            {
                Color c = mat.color;
                c.a = alpha;
                mat.color = c;
            }
            yield return null;
        }
    }

    void SetMaterialTransparent(Material mat)
    {
        mat.SetFloat("_Mode", 2);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
    }
}
