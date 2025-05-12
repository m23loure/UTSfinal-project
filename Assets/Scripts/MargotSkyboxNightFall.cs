using UnityEngine;

public class MargotSkyboxNightfall : MonoBehaviour
{
    public Transform player;
    public Transform startPosition;
    public Transform endPosition;

    public Material daySkybox;  // Skybox de jour
    public Material nightSkybox; // Skybox de nuit

    public Light directionalLight;

    [Header("Ambiance")]
    public Color dayAmbient = Color.white;
    public Color nightAmbient = new Color(0.05f, 0.05f, 0.1f);

    [Header("Lumière directionnelle")]
    public float dayIntensity = 1f;
    public float nightIntensity = 0f;
    public Color dayLightColor = Color.white;
    public Color nightLightColor = new Color(0.1f, 0.1f, 0.2f);

    void Update()
    {
        float totalDistance = Vector3.Distance(startPosition.position, endPosition.position);
        float playerDistance = Vector3.Distance(player.position, startPosition.position);
        float t = Mathf.Clamp01(playerDistance / totalDistance);

        // Change l’ambiance
        RenderSettings.ambientLight = Color.Lerp(dayAmbient, nightAmbient, t);

        // Change la Directional Light
        if (directionalLight != null)
        {
            directionalLight.intensity = Mathf.Lerp(dayIntensity, nightIntensity, t);
            directionalLight.color = Color.Lerp(dayLightColor, nightLightColor, t);
        }

        // Change la Skybox
        RenderSettings.skybox = Mathf.Lerp(0f, 1f, t) > 0.5f ? nightSkybox : daySkybox;

        // Pour avoir un bel effet de transition en douceur, on peut aussi interpoler les couleurs de la Skybox
        if (RenderSettings.skybox == nightSkybox)
        {
            float blend = Mathf.InverseLerp(0.5f, 1f, t);
            RenderSettings.skybox.SetColor("_Tint", Color.Lerp(Color.white, Color.black, blend));
        }
    }
}
