using System;
using UnityEngine;
using UnityEngine.UI;

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

        public int Id { get; private set; }

        public event Action<Card> OnCardClicked;

        public void Initialize (int id)
        {
            name = $"Card_{id}";
            Id = id;
            frontImage.sprite = configuration.cardFrontSprites [id];
            button.onClick.AddListener (OnButtonClicked);

            SetFaceDown ();
        }

        private void OnDestroy ()
        {
            button.onClick.RemoveListener (OnButtonClicked);
        }

        private void OnButtonClicked ()
        {
            OnCardClicked?.Invoke (this);
        }

        public void SetFaceUp ()
        {
            frontImage.gameObject.SetActive (true);
            backImage.gameObject.SetActive (false);

            audioSource.PlayOneShot (configuration.cardFlipSound, configuration.soundVolume);
        }

        public void SetFaceDown ()
        {
            frontImage.gameObject.SetActive (false);
            backImage.gameObject.SetActive (true);
        }

        public void MarkAsMatched ()
        {
            button.interactable = false;

            frontImage.gameObject.SetActive (false);
            backImage.gameObject.SetActive (false);
        }
    }
}