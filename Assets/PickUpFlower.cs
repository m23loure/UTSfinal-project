using UnityEngine;

public class PickUpFlower : MonoBehaviour
{
    public GameObject target;   // The flower object
    public GameObject hand;     // The HandTarget object

    private Animator anim;
    private bool hasPickedUp = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TriggerPickUp()
    {
        anim.SetTrigger("PickUp");
        hasPickedUp = true;
        Debug.Log("TriggerPickUp: Animator Trigger fired, start syncing flower.");
    }

    void LateUpdate()
    {
        if (hasPickedUp)
        {
            target.transform.position = hand.transform.position;
            target.transform.rotation = hand.transform.rotation;
        }
    }
}
