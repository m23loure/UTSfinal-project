using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PostStoneSequence : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public NavMeshAgent agent;

    [Header("Checkpoints")]
    public Transform walkBackTarget;    // A few steps back from the stone
    public Transform checkpoint5;       // Where she trips
    public Transform postJogCheckpoint; // Intermediate run waypoint
    public Transform checkpoint6;       // Final run destination

    [Header("Speeds")]
    public float walkSpeed = 1.2f;
    public float runSpeed  = 3.5f;
    public float jogSpeed  = 1.8f;

    void Start()
    {
        // Only starts once enabled by CharacterSequence
        StartCoroutine(PostStoneRoutine());
    }

    IEnumerator PostStoneRoutine()
    {
        // 1) Idle at stone
        animator.applyRootMotion = false;
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(1.5f);

        // 2) Walk backward
        yield return WalkTo(walkBackTarget.position, walkSpeed, "Walking Backward");

        // 3) Run to checkpoint5
        yield return WalkTo(checkpoint5.position, runSpeed, "Running");

        // 4) Trip/StandUp/InjuredJog using root‐motion
        agent.updatePosition     = false;
        agent.updateRotation     = false;
        animator.applyRootMotion = true;

        animator.SetTrigger("Tripping");
        yield return WaitForAnimation("Tripping");

        animator.SetTrigger("Standing Up");
        yield return WaitForAnimation("Standing Up");

        animator.SetTrigger("Injured Jog");
        yield return WaitForAnimation("Injured Jog");

        // 5) Restore agent & run in two stages
        animator.applyRootMotion = false;
        agent.updatePosition     = true;
        agent.updateRotation     = true;

        yield return WalkTo(postJogCheckpoint.position, runSpeed, "Running");
        yield return WalkTo(checkpoint6.position, runSpeed, "Running");

        // 6) Final Sad (optional)
        animator.SetTrigger("Sad");
    }

    IEnumerator WalkTo(Vector3 dest, float speed, string trigger)
    {
        animator.applyRootMotion = false;
        animator.SetTrigger(trigger);
        agent.speed     = speed;
        agent.isStopped = false;
        agent.SetDestination(dest);
        yield return new WaitUntil(() => agent.remainingDistance < 0.2f && !agent.pathPending);
        agent.isStopped = true;
    }

    IEnumerator WaitForAnimation(string stateName)
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            yield return null;
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;
    }

    void OnAnimatorMove()
    {
        if (animator.applyRootMotion)
        {
            Vector3 delta = animator.deltaPosition;
            // clamp vertical if desired: delta.y = 0f;
            transform.position += delta;
            transform.rotation = animator.rootRotation;
        }
    }
}
