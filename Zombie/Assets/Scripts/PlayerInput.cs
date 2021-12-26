using UnityEngine;

// 플레이어 캐릭터를 조작하기 위한 사용자 입력을 감지
// 감지된 입력값을 다른 컴포넌트들이 사용할 수 있도록 제공
public class PlayerInput : MonoBehaviour {
    // Unity Input에 할당한 입력 축, 버튼은 Input Manager에서 확인가능.
    public string moveAxisName = "Vertical"; // 앞뒤 움직임을 위한 입력축 이름
    public string rotateAxisName = "Horizontal"; // 좌우 회전을 위한 입력축 이름
    public string fireButtonName = "Fire1"; // 발사를 위한 입력 버튼 이름
    public string reloadButtonName = "Reload"; // 재장전을 위한 입력 버튼 이름

    // property 값 할당은 내부에서만 가능
    public float move { get; private set; } // 감지된 움직임 입력값
    public float rotate { get; private set; } // 감지된 회전 입력값
    public bool fire { get; private set; } // 감지된 발사 입력값
    public bool reload { get; private set; } // 감지된 재장전 입력값

    // 매프레임 사용자 입력을 감지
    private void Update() {
        // 게임오버 상태에서는 사용자 입력을 감지하지 않는다
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            move = 0;
            rotate = 0;
            fire = false;
            reload = false;
            return;
        }

        
        // move에 관한 입력 감지
        move = Input.GetAxis(moveAxisName); // GetAxis(string) : 입력으로 감지할 축 이름을 받아 감지된 입력을 숫자로 반환
        // rotate에 관한 입력 감지
        rotate = Input.GetAxis(rotateAxisName);
        // fire에 관한 입력 감지
        fire = Input.GetButton(fireButtonName); // GetButton(string) : 입력으로 감지할 버튼 이름을 받아 해당 버튼을 누르고 있는 동안엔 true 아니면 false
        // reload에 관한 입력 감지
        reload = Input.GetButtonDown(reloadButtonName); // GetButtonDown(string) : 버튼을 누르기 시작한 순간만 true
    }
}