using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MargotCameraScript : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; // Correction de SerializedField en SerializeField

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnTriggerEnter devrait être OnTriggerEnter avec une majuscule
    private void OnTriggerEnter(Collider other) // Correction de Collision en Collider et majuscule
    {
        
        if (other.CompareTag("Player")) // Utilisation de CompareTag pour vérifier l'étiquette
        {
            
            Camera.main.transform.position = cameraTransform.position; // Positionnement de la caméra
            Camera.main.transform.rotation = cameraTransform.rotation; // Rotation de la caméra
        }
    }
}
