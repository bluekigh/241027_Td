using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Vector3 target; // 카메라가 바라볼 타겟
    public float distance = 5.0f; // 카메라와 타겟 간의 거리
    public float sensitivity = 5.0f; // 드래그 민감도

    private float angleX = 0.0f; // 수평 회전 각도
    private float angleY = 0.0f; // 수직 회전 각도

    void Start()
    {
        // 초기 각도 설정
        Vector3 direction = transform.position - target;
        angleX = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        angleY = Mathf.Atan2(direction.y, direction.magnitude) * Mathf.Rad2Deg;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(0)) // 마우스 왼쪽 버튼 클릭 시
        {
            angleX += Input.GetAxis("Mouse X") * sensitivity;
            angleY -= Input.GetAxis("Mouse Y") * sensitivity;
            angleY = Mathf.Clamp(angleY, -80f, 80f); // 수직 회전 범위 제한
        }

        // 타겟을 중심으로 회전
        Quaternion rotation = Quaternion.Euler(angleY, angleX, 0);
        Vector3 position = target - rotation * Vector3.forward * distance;
        if (position.y < 2) position.y = 2;
        transform.position = position; // 카메라 위치 업데이트
        transform.LookAt(target); // 타겟 바라보기
    }
}
