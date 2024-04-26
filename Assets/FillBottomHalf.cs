using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillBottomHalf : MonoBehaviour
{
    void Start()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found.");
            return;
        }

        // ��������� ������ ������ ������
        float cameraWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;

        // ������ �������� ������ �� ������ ������
        transform.localScale = new Vector3(cameraWidth, cameraWidth, 1f);

        // �������� ���������� Y ��� �������������� �������� ����� ������
        float cameraBottomY = mainCamera.transform.position.y - mainCamera.orthographicSize;

        // ������ ������� ����� ������
        transform.position = new Vector3(mainCamera.transform.position.x, cameraBottomY + cameraWidth / 2f, transform.position.z);
    }
}