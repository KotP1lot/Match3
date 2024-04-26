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

        // Отримання розмірів екрану камери
        float cameraWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;

        // Задаємо квадрату ширину та висоту камери
        transform.localScale = new Vector3(cameraWidth, cameraWidth, 1f);

        // Отримуємо координату Y для позиціонування квадрата знизу камери
        float cameraBottomY = mainCamera.transform.position.y - mainCamera.orthographicSize;

        // Зміщуємо квадрат внизу камери
        transform.position = new Vector3(mainCamera.transform.position.x, cameraBottomY + cameraWidth / 2f, transform.position.z);
    }
}