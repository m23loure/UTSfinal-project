using UnityEngine;

public class JulieSignalHandler : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnJumpSignal()
    {
        animator.SetTrigger("triggerJump"); // <- Animator 파라미터 중 Trigger 이름
    }
}
