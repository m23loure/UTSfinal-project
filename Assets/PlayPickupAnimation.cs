using UnityEngine;

public class PlayPickupAnimation : MonoBehaviour
{
    public string triggeringTag = "Player"; // 줄리에게 "Player" 태그가 있어야 함
    public Animator julieAnimator; // 줄리의 Animator 연결
    public string triggerName = "PickUp"; // 애니메이터에서 설정한 Trigger 이름

    private bool hasPlayed = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag(triggeringTag))
        {
            if (julieAnimator != null)
            {
                julieAnimator.SetTrigger(triggerName);
                hasPlayed = true;
            }
            else
            {
                Debug.LogWarning("Animator not assigned!");
            }
        }
    }
}
