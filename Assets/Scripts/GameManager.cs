using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SymmetricalOctoEureka
{
    public class GameManager : MonoBehaviour
    {
        [Header ("Panels")]
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject gameOverPanel;

        [Header ("Texts")]
        [SerializeField] private TMP_Text turnsText;
        [SerializeField] private TMP_Text matchesText;
        [SerializeField] private TMP_Text streakText;
        [SerializeField] private TMP_Text scoreText;

        [Header ("Buttons")]
        [SerializeField] private Button backButton;

        [Header ("BoardManager")]
        [SerializeField] private BoardManager boardManager;

        [Header ("Configuration")]
        [SerializeField] private Configuration configuration;

        private GameState currentGameState;
        private Card firstSelectedCard;
        private Card secondSelectedCard;
        private bool canInteract;

        public void StartNewGame (int rows, int columns)
        {
            currentGameState = new GameState (rows, columns);

            InitializeGame ();
        }

        public void LoadSavedGame ()
        {
            currentGameState = new GameState (3, 4);
            currentGameState.turns = 4;

            InitializeGame ();
        }

        private void InitializeGame ()
        {
            UpdateTexts ();

            boardManager.SetupBoard (currentGameState, OnCardClicked);

            if (currentGameState.IsGameRunning)
            {
                canInteract = true;
            }
            else
            {
                StartCoroutine (StartGameSequence ());
            }
        }

        private void UpdateTexts ()
        {
            turnsText.text = $"{currentGameState.turns}";
            matchesText.text = $"{currentGameState.matches}";
            streakText.text = $"{currentGameState.streak}";
            scoreText.text = $"{currentGameState.score}";
        }

        private IEnumerator StartGameSequence ()
        {
            canInteract = false;

            yield return new WaitForSeconds (1f);

            boardManager.SetAllFaceUp ();

            yield return new WaitForSeconds (configuration.initialPeekDuration);

            boardManager.SetAllFaceDown ();

            canInteract = true;
        }

        private void OnCardClicked (Card clickedCard)
        {
            if (!canInteract || clickedCard == null || clickedCard == firstSelectedCard)
            {
                return;
            }

            clickedCard.SetFaceUp ();

            if (firstSelectedCard == null)
            {
                firstSelectedCard = clickedCard;
            }
            else
            {
                secondSelectedCard = clickedCard;

                currentGameState.turns += 1;

                UpdateTexts ();

                StartCoroutine (ProcessCardMatch ());
            }
        }

        private IEnumerator ProcessCardMatch ()
        {
            canInteract = false;

            bool isMatch = firstSelectedCard.Id == secondSelectedCard.Id;

            if (isMatch)
            {
                yield return ProcessMatchedCards ();
            }
            else
            {
                yield return ProcessMismatchedCards ();
            }

            UpdateTexts ();

            if (currentGameState.IsGameComplete)
            {
                GameOver ();
            }
            else
            {
                SaveGame ();
            }

            firstSelectedCard = null;
            secondSelectedCard = null;
            canInteract = true;
        }

        private IEnumerator ProcessMatchedCards ()
        {
            yield return new WaitForSeconds (configuration.matchDelay);

            firstSelectedCard.MarkAsMatched ();
            secondSelectedCard.MarkAsMatched ();

            currentGameState.AddMatchedCard (firstSelectedCard.Id);
            currentGameState.AddMatchedCard (secondSelectedCard.Id);
            currentGameState.matches += 1;
            currentGameState.streak += 1;
            currentGameState.score += currentGameState.streak;
        }

        private IEnumerator ProcessMismatchedCards ()
        {
            yield return new WaitForSeconds (configuration.mismatchDelay);

            firstSelectedCard.SetFaceDown ();
            secondSelectedCard.SetFaceDown ();

            currentGameState.streak = 0;
        }

        private void GameOver ()
        {
            gameOverPanel.SetActive (true);
        }

        private void SaveGame ()
        {

        }

        private void ResetGame ()
        {
            currentGameState = null;
            firstSelectedCard = null;
            secondSelectedCard = null;
            canInteract = false;

            gameOverPanel.SetActive (false);

            boardManager.ClearBoard (OnCardClicked);
        }

        private void Start ()
        {
            backButton.onClick.AddListener (OnBackButtonClicked);
        }

        private void OnDestroy ()
        {
            backButton.onClick.RemoveListener (OnBackButtonClicked);
        }

        private void OnBackButtonClicked ()
        {
            SwitchToMainMenu ();
        }

        private void SwitchToMainMenu ()
        {
            mainPanel.SetActive (true);
            gamePanel.SetActive (false);

            ResetGame ();
        }
    }
}