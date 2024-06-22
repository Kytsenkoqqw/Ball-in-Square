using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class ChallengPanelAnimation : MonoBehaviour
{
   [SerializeField] private GameObject _challengePanel;
   [SerializeField] private RectTransform _rectTransform;
   [SerializeField] private GameObject _closedChallengeButton;

   private void Start()
   {
     // _closedChallengeButton.SetActive(false);
      _challengePanel.SetActive(false);
      _rectTransform = GetComponent<RectTransform>();
      _rectTransform.anchoredPosition = new Vector2(0, Screen.height);
      BallController.OnPlayerDied += OnPlayerDied;
   }

   private void OnPlayerDied()
   {
      _rectTransform.gameObject.SetActive(true);
      ShowChallengePanel();
   }

   private void OnDestroy()
   {
      BallController.OnPlayerDied -= OnPlayerDied;
   }

   private void ShowChallengePanel()
   {
      _rectTransform.DOAnchorPos(new Vector2(0, 1875),  0.5f);
   }

   public void OpenAnimateChallengePanel()
   {
      _closedChallengeButton.SetActive(true);
      _rectTransform.DOAnchorPos(new Vector2(0, 0),  0.5f);
   }

   public void ClosedAnimateChallengePanel()
   {
      _rectTransform.DOAnchorPos(new Vector2(0, Screen.height),  0.5f);
   }

}
