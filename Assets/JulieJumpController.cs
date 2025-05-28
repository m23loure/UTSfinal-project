using UnityEngine;
using UnityEngine.Playables;

public class JulieJumpController : MonoBehaviour
{
    public Animator animator;
    public PlayableDirector timeline;

    private bool timelineEnded = false;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (timeline != null)
        {
            timeline.stopped += OnTimelineStopped;
        }
    }

    void Update()
    {
        // 타임라인이 끝났고, J 키를 누르면 점프
        if (timelineEnded && Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("triggerJump");
        }
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        // Timeline 끝났을 때만 점프 허용
        timelineEnded = true;
        Debug.Log("Timeline finished, jump is now allowed.");
    }
}
