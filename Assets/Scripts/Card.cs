using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace SymmetricalOctoEureka
{
    public class Card : MonoBehaviour
    {
        [Header ("Images")]
        [SerializeField] public Image frontImage;
        [SerializeField] private Image backImage;

        [Header ("Buttons")]
        [SerializeField] private Button button;

        [Header ("AudioSources")]
        [SerializeField] private AudioSource audioSource;

        [Header ("Configuration")]
        [SerializeField] private Configuration configuration;

        [Header ("Animation")]
        [SerializeField] private RectTransform rectTransform;

        public int Id { get; private set; }

        public event Action<Card> OnCardClicked;

        private Sequence activeSequence;
        private bool isAnimating;

        public void Initialize (int id)
        {
            name = $"Card_{id}";
            Id = id;
            frontImage.sprite = configuration.cardFrontSprites [id];
            backImage.sprite = configuration.cardBackSprite;
            button.onClick.AddListener (OnButtonClicked);
        }

        private void OnDestroy ()
        {
            button.onClick.RemoveListener (OnButtonClicked);
            activeSequence?.Kill ();
        }

        private void OnButtonClicked ()
        {
            if (isAnimating) return;

            OnCardClicked?.Invoke (this);
        }

        public void SetFaceUp () => Flip (true);
        public void SetFaceDown () => Flip (false);

        private void Flip (bool showFront)
        {
            isAnimating = true;
            activeSequence?.Kill ();

            activeSequence = DOTween.Sequence ();
            activeSequence.Append (rectTransform.DOScaleX (0f, configuration.flipDuration));
            activeSequence.AppendCallback (() =>
            {
                frontImage.gameObject.SetActive (showFront);
                backImage.gameObject.SetActive (!showFront);

                if (showFront) audioSource.PlayOneShot (configuration.cardFlipSound, configuration.soundVolume);
            });
            activeSequence.Append (rectTransform.DOScaleX (1f, configuration.flipDuration));
            activeSequence.OnComplete (() => isAnimating = false);
        }

        public void SetMatched ()
        {
            button.interactable = false;

            isAnimating = true;
            activeSequence?.Kill ();

            activeSequence = DOTween.Sequence ();
            activeSequence.Append (rectTransform.DOScale (0f, configuration.scaleDuration));
            activeSequence.OnComplete (() => isAnimating = false);
        }

        public void Shake ()
        {
            isAnimating = true;
            activeSequence?.Kill ();

            activeSequence = DOTween.Sequence ();
            activeSequence.Append (rectTransform.DOShakePosition (configuration.shakeDuration, configuration.shakeStrength));
            activeSequence.OnComplete (SetFaceDown);
        }
    }
}