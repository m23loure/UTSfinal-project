using UnityEngine;

public class MargotTransparentTrees : MonoBehaviour
{
    public Shader replacementShader;

    void Start()
    {
        // Assure-toi que le shader est bien assigné
        if (replacementShader == null)
        {
            Debug.LogError("Assign a transparent shader in the inspector.");
            return;
        }

        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
        foreach (Renderer rend in renderers)
        {
            foreach (Material mat in rend.materials)
            {
                // Remplace uniquement si le shader actuel est opaque ou non-compatible
                if (!mat.shader.name.Contains("Transparent"))
                {
                    mat.shader = replacementShader;

                    // Active le mode transparent s’il s’agit d’un shader standard
                    if (replacementShader.name == "Universal Render Pipeline/Lit")
                    {
                        mat.SetFloat("_Surface", 1); // 0 = Opaque, 1 = Transparent
                        mat.SetFloat("_Blend", 0);
                        mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
                        mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    }
                }
            }
        }
    }
}
