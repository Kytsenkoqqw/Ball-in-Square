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
        _rectTransform.anchoredPosition = new Vector2(0, -Screen.height);
        BallController.OnPlayerDied += AnimatePanel;
    }

    private void OnDestroy()
    {
        BallController.OnPlayerDied -= AnimatePanel;
    }

    private void AnimatePanel()
    {
        _rectTransform.DOAnchorPos(new Vector2(0,0), 0.5f);
    }
}
