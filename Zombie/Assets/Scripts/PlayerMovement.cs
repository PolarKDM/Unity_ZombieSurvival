using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도


    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    private void Start() {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate() {
        // 물리 갱신 주기마다 움직임, 회전, 애니메이션 처리 실행
        Rotate();
        Move();

        // 입력값에 따라 애니메이터의 Move 파라미터값 변경
        playerAnimator.SetFloat("Move", playerInput.move);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move() {
        // 상대적으로 이동할 거리 계산 (사용자입력값 * 방향 * 속력 * 시간)
        Vector3 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        // 리지드바디를 이용해 게임 오브젝트 위치 변경.
        // MovePosition(Vector3) : 해당 좌표(전역좌표)로 이동
        // 전역좌표 = playerRigidbody.position(현재좌표) + moveDistance(상대좌표)
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);

        // transform 컴포넌트를 사용하지않은 이유 : 벽에 닿을시 충돌을 무시함.
        // Rigidbody.MovePosition() : 위치값을 변경하면 이동경로에 다른 콜라이더 존재시 밀어내거나 밀려나는 물리처리 실행
    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void Rotate() {
        // 상대적으로 회전할 수치 계산 (사용자입력값 * 회전속도 * 시간)
        float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;
        // 리지드바디를 이용해 게임 오브젝트 회전 변경
        // 어떤 회전상태에서 상대적으로 더 회전 -> Quaternion 사용 (현재회전상태 * (0, turn, 0f)만큼 더 회전)
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0, turn, 0f);
    }
}