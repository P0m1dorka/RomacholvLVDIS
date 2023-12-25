using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFLow : MonoBehaviour
{
    public Transform player; // Ссылка на объект игрока
    public float smoothness = 0.5f; // Плавность движения камеры

    private Vector3 offset; // Смещение камеры относительно игрока

    void Start()
    {
        offset = transform.position - player.position; // Вычисляем начальное смещение
    }

    void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset; // Вычисляем позицию, к которой должна двигаться камера
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness); // Интерполируем текущую позицию к целевой позиции с плавностью
    }
}
