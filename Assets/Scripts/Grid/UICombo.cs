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
            // ��������� ��� � ��������� ���������
            text.transform.localScale = Vector3.one;
            text.transform.localRotation = Quaternion.identity;

            // ��������� ��������� ������� ������� �� �������� obj
            float scaleAmount = 1 + (obj - 3) * 0.1f; // �������� ����� �� 0.1 �� ������ obj ����� �� 3
            float scaleDuration = 0.2f; // ��������� ������� ��������� �� ��������� ������ ������

            float rotationAmount = 5f + (obj - 3) * 2f; // ������ ��� �������� � ��������� �� obj
            float rotationDuration = 0.2f; // ��������� ������� ��������
            sequence.Kill();
            // ��������� ���� ����������� �������
            sequence = DOTween.Sequence();

            //// ������� ��������� �� ��������� ������ ������
            sequence.Append(text.transform.DOScale(scaleAmount, scaleDuration))
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo); // ������ ����� � �������� ����������

            //������� �������� ������
            sequence.Append(text.transform.DORotate(new Vector3(0, 0, rotationAmount), rotationDuration))
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo); // ������ ����� � �������� ����������
        }
        else
        {
            
            text.enabled = false;
        }
    }
}
