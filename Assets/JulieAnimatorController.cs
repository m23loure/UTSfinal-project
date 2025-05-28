using UnityEngine;
using UnityEngine.AI;

public class JulieAnimatorController : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public Transform walkTarget;

    private bool hasJumped = false; // 점프 한 번만 실행하기 위한 플래그

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        agent.speed = 1.2f;
    }

    void Update()
    {
        if (agent.enabled && walkTarget != null)
        {
            float distance = agent.remainingDistance;

            // 아직 점프 안했고, 목적지 거의 도착했으면
            if (!hasJumped && distance <= agent.stoppingDistance + 0.1f && agent.velocity.magnitude < 0.1f)
            {
                animator.SetBool("isWalking", false);
                animator.SetTrigger("triggerJump");
                hasJumped = true;  // 점프 상태 진입 표시
            }
            else if (distance > agent.stoppingDistance + 0.1f)
            {
                // 걷는 상태 유지
                animator.SetBool("isWalking", true);
                hasJumped = false; // 목적지 가는 중이니 점프 상태 해제
            }
        }
    }

    public void StartWalking()
    {
        if (walkTarget != null && agent != null)
        {
            agent.SetDestination(walkTarget.position);
            animator.SetBool("isWalking", true);
            hasJumped = false;
        }
    }

    public void StartJump()
    {
        animator.SetTrigger("triggerJump");
        hasJumped = true;  // Signal로 점프 호출 시에도 플래그 처리
    }

    public void StartPickUp()
    {
        animator.SetTrigger("triggerPickup");
    }
}
