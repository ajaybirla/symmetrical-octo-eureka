using UnityEngine;
using UnityEngine.UI;

namespace SymmetricalOctoEureka
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private int cardID;
        [SerializeField] private Image cardFront;
        [SerializeField] private Image cardBack;
        [SerializeField] private Button button;
        [SerializeField] GameManager gameManager;

        public int CardID => cardID;

        private void Start ()
        {
            button.onClick.AddListener (OnCardClicked);

            cardFront.gameObject.SetActive (false);
            cardBack.gameObject.SetActive (true);
        }

        private void OnCardClicked ()
        {
            if (cardFront.gameObject.activeSelf || !gameManager.CanClick) return;

            Flip ();
            gameManager.CardFlipped (this);
        }

        public void Flip ()
        {
            bool isFlipped = cardFront.gameObject.activeSelf;
            cardFront.gameObject.SetActive (!isFlipped);
            cardBack.gameObject.SetActive (isFlipped);
        }
    }
}