using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SymmetricalOctoEureka
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<Card> allCards;

        private Card firstSelected;
        private Card secondSelected;

        public bool CanClick { get; private set; } = true;

        public void CardFlipped (Card card)
        {
            if (firstSelected == null)
            {
                firstSelected = card;
            }
            else
            {
                secondSelected = card;
                StartCoroutine (CheckMatch ());
            }
        }

        IEnumerator CheckMatch ()
        {
            CanClick = false;

            if (firstSelected.CardID == secondSelected.CardID)
            {
                firstSelected = null;
                secondSelected = null;
            }
            else
            {
                yield return new WaitForSeconds (1f);

                firstSelected.Flip ();
                secondSelected.Flip ();

                firstSelected = null;
                secondSelected = null;
            }

            CanClick = true;
        }
    }
}