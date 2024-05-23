using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class UICombo : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Sequence sequence;
    public void Start()
    {
        EventManager.instance.OnComboChanged += ChangeCombo;
        text = GetComponent<TextMeshProUGUI>();
    }

    private void ChangeCombo(int obj)
    {
        if (obj > 3)
        {
            text.enabled = true;
            text.text = $"COMBO x{obj}!";
            // Повертаємо тіло у початкове положення
            text.transform.localScale = Vector3.one;
            text.transform.localRotation = Quaternion.identity;

            // Визначаємо параметри анімацій залежно від значення obj
            float scaleAmount = 1 + (obj - 3) * 0.1f; // Збільшуємо розмір на 0.1 за кожний obj більше за 3
            float scaleDuration = 0.2f; // Тривалість анімації збільшення та зменшення розміру тексту

            float rotationAmount = 5f + (obj - 3) * 2f; // Додамо кут повороту в залежності від obj
            float rotationDuration = 0.2f; // Тривалість анімації повороту
            sequence.Kill();
            // Створюємо нову послідовність анімацій
            sequence = DOTween.Sequence();

            //// Анімація збільшення та зменшення розміру тексту
            sequence.Append(text.transform.DOScale(scaleAmount, scaleDuration))
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo); // Хитаємо текст з плавними переходами

            //Анімація повороту тексту
            sequence.Append(text.transform.DORotate(new Vector3(0, 0, rotationAmount), rotationDuration))
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo); // Хитаємо текст з плавними переходами
        }
        else
        {
            
            text.enabled = false;
        }
    }
}
