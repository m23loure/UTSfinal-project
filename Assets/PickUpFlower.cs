using UnityEngine;

public class PickUpFlower : MonoBehaviour
{
    public GameObject target;   // 줍는 대상 (꽃)
    public GameObject hand;     // 손 오브젝트 (부착할 위치)
    public CharacterController controller; // 이동 컨트롤러
    public float moveSpeed = 5f;

    private Animator anim;
    private bool canMove = true;
    private float IK_weight = 0.0f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 줍기 입력 처리
        if (Input.GetKeyDown(KeyCode.Space) && canMove)
        {
            canMove = false; // 이동 잠금
            anim.SetTrigger("PickUp");
        }

        // 줍기 애니메이션이 끝나고 Idle 상태로 돌아오면 이동 다시 허용
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle") && !canMove)
        {
            canMove = true;
        }

        // 이동 처리
        if (canMove)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(h, 0, v);
            controller.Move(move * moveSpeed * Time.deltaTime);
        }
    }

    // IK 처리
    private void OnAnimatorIK(int layerIndex)
    {
        IK_weight = anim.GetFloat("IKPickUp");

        // 손에 붙이기 처리
        if (IK_weight > 0.95f && target.transform.parent != hand.transform)
        {
            target.transform.SetParent(hand.transform);
            target.transform.localPosition = Vector3.zero;
            target.transform.localRotation = Quaternion.identity;
        }

        // 손과 시선 IK 설정
        anim.SetIKPosition(AvatarIKGoal.RightHand, target.transform.position);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, IK_weight);
        anim.SetLookAtPosition(target.transform.position);
        anim.SetLookAtWeight(IK_weight);
    }

    // (선택) 애니메이션 이벤트용 함수
    public void EnableMovement()
    {
        canMove = true;
    }
}
