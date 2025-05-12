using UnityEngine;

public class MargotLightFadeByPosition : MonoBehaviour
{
    public Transform player;
    public Light sceneLight;
    public Transform startPosition; // début de la transition
    public Transform endPosition;   // fin de la transition
    public float minIntensity = 0.1f;
    public float maxIntensity = 1f;

    void Update()
    {
        // Distance totale entre start et end
        float totalDistance = Vector3.Distance(startPosition.position, endPosition.position);
        // Distance du joueur par rapport à start
        float playerDistance = Vector3.Distance(player.position, startPosition.position);

        // T = 0 (au début), T = 1 (à la fin ou au-delà)
        float t = Mathf.Clamp01(playerDistance / totalDistance);

        // On interpole l’intensité
        float newIntensity = Mathf.Lerp(maxIntensity, minIntensity, t);
        sceneLight.intensity = newIntensity;
    }
}
