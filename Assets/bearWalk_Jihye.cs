using UnityEngine;

public class BearWalk_Jihye : MonoBehaviour
{
    private Animator animator;

    [Tooltip("Julie to follow")]
    public Transform julie; // Julie is now passed as a reference

    [Tooltip("Minimum distance to maintain with Julie")]
    public float stopDistance = 1f;

    [Tooltip("Maximum walking speed")]
    public float maxSpeed = 10f;

    [Tooltip("Rotation interpolation speed")]
    public float rotationSpeed = 5f;

    private void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetBool("isWalking", true); // Start walking
        }
    }

    private void Update()
    {
        if (julie == null) return;

        // Calculate the direction and distance to Julie
        Vector3 direction = julie.position - transform.position;
        direction.y = 0; // Ignore vertical differences

        float distance = direction.magnitude;

        if (distance > stopDistance)
        {
            // Smooth rotation towards Julie
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Dynamically adjust speed so the bear doesn't overshoot
            float adjustedSpeed = Mathf.Min(maxSpeed, distance - stopDistance); // No Time.deltaTime here
            Vector3 moveDirection = direction.normalized * adjustedSpeed;
            transform.position += moveDirection * Time.deltaTime; // Only apply Time.deltaTime once

            if (animator != null)
            {
                animator.SetBool("isWalking", true);
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("isWalking", false);
            }
        }
    }
}
