using UnityEngine;

public class BearWalk_Jihye : MonoBehaviour
{
    private Animator animator;
    private Transform player;

    public float followDistance = 10f;      // 최대 추적 거리
    public float minDistance = 2f;          // 최소 거리(이보다 가까우면 천천히 움직임)
    public float moveSpeed = 2f;            // 최대 속도
    public float rotationSpeed = 5f;        // 회전 속도

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < followDistance)
        {
            animator.SetBool("isWalking", true);

            // 회전 방향 계산
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            // 거리 기반으로 속도 보간 조절
            float adjustedSpeed = Mathf.Lerp(0f, moveSpeed, (distance - minDistance) / (followDistance - minDistance));
            adjustedSpeed = Mathf.Clamp(adjustedSpeed, 0.1f, moveSpeed * 0.9f); // 최소속도 유지

            // 이동
            transform.position += transform.forward * adjustedSpeed * Time.deltaTime;
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
