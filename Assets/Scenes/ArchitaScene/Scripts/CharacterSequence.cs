using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CharacterSequence : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public NavMeshAgent agent;

    [Header("Checkpoints")]
    public Transform julieStartPoint;
    public Transform secondCheckpoint;
    public Transform thirdCheckpoint;
    public Transform[] runWaypoints;
    public Transform stoneTarget;

    [Header("Post-Stone Sequence")]
    public Transform walkBackTarget;
    public Transform checkpoint5;
    public Transform checkpoint6;   // where injured jog ends
    public Transform checkpoint7;   // where running resumes

    [Header("Speeds & Timing")]
    public float walkSpeed        = 1.2f;
    public float runSpeed         = 3.5f;
    public float jogSpeed         = 1.8f;
    public float pivotDelay       = 1f;
    public float sadPause         = 2f;
    public float rotationDuration = 0.5f;

    void Start()
    {
        animator.applyRootMotion = false;
        agent.enabled            = true;
        agent.isStopped          = true;
        StartCoroutine(AnimationSequence());
    }

    IEnumerator AnimationSequence()
    {
        // 1) Initial placement & Sad intro
        transform.position = julieStartPoint.position;
        transform.rotation = julieStartPoint.rotation;
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("Sad");
        yield return new WaitForSeconds(sadPause);

        // 2) Sad Walk → second checkpoint
        yield return WalkTo(secondCheckpoint.position, walkSpeed, "Sad Walk");

        // 3) Pivot to third checkpoint (Sad)
        yield return RotateToFace(thirdCheckpoint.position);
        animator.SetTrigger("Sad");
        yield return new WaitForSeconds(pivotDelay);

        // 4) Pivot back to start (Sad)
        yield return RotateToFace(julieStartPoint.position);
        animator.SetTrigger("Sad");
        yield return new WaitForSeconds(pivotDelay);

        // 5) Sad Walk back to start
        yield return WalkTo(julieStartPoint.position, walkSpeed, "Sad Walk");

        // 6) Running path out to the stone
        animator.SetTrigger("Running");
        agent.speed    = runSpeed;
        agent.isStopped = false;
        foreach (var wp in runWaypoints)
        {
            agent.SetDestination(wp.position);
            yield return new WaitUntil(() => agent.remainingDistance < 0.2f && !agent.pathPending);
        }
        agent.SetDestination(stoneTarget.position);
        yield return new WaitUntil(() => agent.remainingDistance < 0.2f && !agent.pathPending);
        agent.isStopped = true;

        // 7) Final Sad at stone
        animator.SetTrigger("Sad");
        yield return new WaitForSeconds(1.5f);

        // === POST-STONE SEQUENCE ===

        // 8) Idle at stone
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(1.5f);

        // 9) Walk backward
        yield return WalkTo(walkBackTarget.position, walkSpeed, "Walking Backward");

        // 10) Run to checkpoint5
        yield return WalkTo(checkpoint5.position, runSpeed, "Running");

        // 11) Trip
        animator.SetTrigger("Tripping");
        yield return new WaitForSeconds(1.8f);

        // 12) Stand up
        animator.SetTrigger("Standing Up");
        yield return new WaitForSeconds(1.8f);

        // 13) Injured Jog → checkpoint6
        animator.SetTrigger("Injured Jog");
        agent.speed    = jogSpeed;
        agent.isStopped = false;
        agent.SetDestination(checkpoint6.position);
        yield return new WaitUntil(() => agent.remainingDistance < 0.2f && !agent.pathPending);
        agent.isStopped = true;

        // 14) Running → checkpoint7
        animator.SetTrigger("Running");
        agent.speed    = runSpeed;
        agent.isStopped = false;
        agent.SetDestination(checkpoint7.position);
        yield return new WaitUntil(() => agent.remainingDistance < 0.2f && !agent.pathPending);
        agent.isStopped = true;

        // 15) Idle
        animator.SetTrigger("Idle");
    }

    IEnumerator WalkTo(Vector3 dest, float speed, string trigger)
    {
        animator.SetTrigger(trigger);
        agent.speed     = speed;
        agent.isStopped = false;
        agent.SetDestination(dest);
        yield return new WaitUntil(() => agent.remainingDistance < 0.2f && !agent.pathPending);
        agent.isStopped = true;
    }

    IEnumerator RotateToFace(Vector3 target)
    {
        agent.updateRotation = false;
        Vector3 dir = target - transform.position;
        dir.y = 0;
        if (dir.sqrMagnitude > 0.0001f)
        {
            Quaternion from = transform.rotation;
            Quaternion to   = Quaternion.LookRotation(dir.normalized);
            float elapsed = 0f;
            while (elapsed < rotationDuration)
            {
                elapsed += Time.deltaTime;
                transform.rotation = Quaternion.Slerp(from, to, elapsed / rotationDuration);
                yield return null;
            }
            transform.rotation = to;
        }
        agent.updateRotation = true;
    }
}
