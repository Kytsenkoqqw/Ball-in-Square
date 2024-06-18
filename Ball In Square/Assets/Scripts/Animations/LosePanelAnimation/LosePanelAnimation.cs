using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LosePanelAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        // Скрываем панель в начале игры (она должна быть за пределами экрана)
        _rectTransform.anchoredPosition = new Vector2(0, -Screen.height);

        // Подписываемся на событие смерти игрока
        BallController.OnPlayerDied += AnimatePanel;
    }

    private void OnDestroy()
    {
        // Обязательно отписываемся от события при уничтожении объекта
        BallController.OnPlayerDied -= AnimatePanel;
    }

    private void AnimatePanel()
    {
        // Анимация выезда панели
        _rectTransform.DOAnchorPos(new Vector2(0,0), 0.5f);
        
    }
}
