using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace SymmetricalOctoEureka
{
    public class BoardManager : MonoBehaviour
    {
        [Header ("Board")]
        [SerializeField] private RectTransform boardRectTransform;
        [SerializeField] private GridLayoutGroup gridLayout;

        [Header ("Configuration")]
        [SerializeField] private Configuration configuration;

        private List<Card> spawnedCards = new List<Card> ();

        public void SetupBoard (GameState gameState, Action<Card> onCardClicked)
        {
            ConfigureGrid (gameState.rows, gameState.columns);
            SpawnCards (gameState);

            foreach (Card card in spawnedCards)
            {
                card.OnCardClicked += onCardClicked;
            }
        }

        public void ClearBoard (Action<Card> onCardClicked)
        {
            foreach (Card card in spawnedCards)
            {
                card.OnCardClicked -= onCardClicked;

                Destroy (card.gameObject);
            }

            spawnedCards.Clear ();
        }

        private void ConfigureGrid (int rows, int columns)
        {
            float availableWidth = boardRectTransform.rect.width;
            float availableHeight = boardRectTransform.rect.height;

            float totalSpacingX = gridLayout.spacing.x * (columns - 1);
            float totalSpacingY = gridLayout.spacing.y * (rows - 1);

            float totalPaddingX = gridLayout.padding.left + gridLayout.padding.right;
            float totalPaddingY = gridLayout.padding.top + gridLayout.padding.bottom;

            float cellWidth = (availableWidth - totalSpacingX - totalPaddingX) / columns;
            float cellHeight = (availableHeight - totalSpacingY - totalPaddingY) / rows;

            float cellSize = Mathf.Min (cellWidth, cellHeight);

            gridLayout.cellSize = new Vector2 (cellSize, cellSize);
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = columns;
        }

        private void SpawnCards (GameState gameState)
        {
            for (int i = 0; i < gameState.layoutCardIds.Count; i++)
            {
                SpawnCard (i, gameState);
            }
        }

        private void SpawnCard (int index, GameState gameState)
        {
            int cardId = gameState.layoutCardIds [index];

            GameObject cardObject = Instantiate (configuration.cardPrefab, gridLayout.transform);
            Card card = cardObject.GetComponent<Card> ();
            card.Initialize (cardId);

            if (gameState.IsCardMatched (index))
            {
                card.MarkAsMatched ();
            }

            spawnedCards.Add (card);
        }

        public void SetAllFaceUp ()
        {
            foreach (Card card in spawnedCards)
            {
                card.SetFaceUp ();
            }
        }

        public void SetAllFaceDown ()
        {
            foreach (Card card in spawnedCards)
            {
                card.SetFaceDown ();
            }
        }
    }
}