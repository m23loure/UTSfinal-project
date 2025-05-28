using UnityEngine;

public class JulieActions : MonoBehaviour
{
    public GameObject flower;

    public void PickUpFlower()
    {
        Debug.Log("Picked up the flower!");
        flower.SetActive(false); // Hide the flower object
        // If you have an animation trigger:
        // animator.SetTrigger("PickUp");
    }
}
