using UnityEngine;
using System.Collections;

public class MargotStormTrigger : MonoBehaviour
{
    public ParticleSystem lightningParticles; // Référence au système de particules des éclairs
    public AudioSource thunderSound; // Son du tonnerre
    public float stormDuration = 10f; // Durée de l'orage
    public float lightningInterval = 1f; // Intervalle entre chaque éclair (en secondes)
    public int numberOfLightning = 5; // Nombre d’éclairs à générer

    private bool stormActive = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !stormActive)
        {
            StartCoroutine(ActivateStorm());
        }
    }

    IEnumerator ActivateStorm()
    {
        stormActive = true;

        for (int i = 0; i < numberOfLightning; i++)
        {
            lightningParticles.Play();

            if (thunderSound != null)
            {
                thunderSound.Play();
            }

            yield return new WaitForSeconds(lightningInterval);
        }

        yield return new WaitForSeconds(stormDuration);

        lightningParticles.Stop();

        if (thunderSound != null)
        {
            thunderSound.Stop();
        }

        stormActive = false;
    }
}
