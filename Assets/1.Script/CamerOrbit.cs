using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // 회전 기준이 되는 지점
    public float distance = 5.0f; // 카메라와 대상 간의 거리
    public float sensitivity = 5.0f; // 드래그 민감도

    private float angleX = 0.0f; // 수직 회전 각도
    private float angleY = 0.0f; // 수평 회전 각도

    void LateUpdate()
    {
        if (Input.GetMouseButton(0)) // 마우스 왼쪽 버튼 클릭 시
        {
            angleX += Input.GetAxis("Mouse X") * sensitivity;
            angleY -= Input.GetAxis("Mouse Y") * sensitivity;
            angleY = Mathf.Clamp(angleY, -80f, 80f); // 수직 회전 범위 제한
        }

        // 회전 계산
        Quaternion rotation = Quaternion.Euler(angleY, angleX, 0);
        Vector3 position = target.position - rotation * Vector3.forward * distance;

        transform.position = position;
        transform.LookAt(target.position); // 항상 대상 바라보기
    }
}
