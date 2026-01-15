using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

        public void SetupBoard (GameState gameState)
        {
            ClearBoard ();
            ConfigureGrid (gameState.rows, gameState.columns);
            SpawnCards (gameState);
        }

        private void ClearBoard ()
        {
            foreach (Card card in spawnedCards)
            {
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
            Sprite cardSprite = configuration.cardFrontSprites [cardId];

            GameObject cardObject = Instantiate (configuration.cardPrefab, gridLayout.transform);
            Card card = cardObject.GetComponent<Card> ();
            card.Initialize (cardId, cardSprite);

            spawnedCards.Add (card);
        }
    }
}