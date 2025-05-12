using UnityEngine;

public class MargotAmbientFade : MonoBehaviour
{
    public Transform player;
    public Transform startPosition;
    public Transform endPosition;

    public Color dayColor = Color.white;
    public Color nightColor = new Color(0.05f, 0.05f, 0.1f); // très sombre, bleuté

    void Update()
    {
        float totalDistance = Vector3.Distance(startPosition.position, endPosition.position);
        float playerDistance = Vector3.Distance(player.position, startPosition.position);
        float t = Mathf.Clamp01(playerDistance / totalDistance);

        // Lerp entre la couleur du jour et de la nuit
        RenderSettings.ambientLight = Color.Lerp(dayColor, nightColor, t);
    }
}
