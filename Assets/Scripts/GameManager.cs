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

        [Header ("BoardManager")]
        [SerializeField] private BoardManager boardManager;

        [Header ("Texts")]
        [SerializeField] private TMP_Text turnsText;
        [SerializeField] private TMP_Text matchesText;
        [SerializeField] private TMP_Text streakText;
        [SerializeField] private TMP_Text scoreText;

        [Header ("Buttons")]
        [SerializeField] private Button backButton;
        [SerializeField] private Button restartButton;

        public void StartNewGame (int rows, int columns)
        {
            GameState newGameState = new GameState (rows, columns);

            boardManager.SetupBoard (newGameState);
        }

        public void LoadSavedGame ()
        {

        }

        private void Start ()
        {
            backButton.onClick.AddListener (OnBackButtonClicked);
            restartButton.onClick.AddListener (OnRestartButtonClicked);
        }

        private void OnDestroy ()
        {
            backButton.onClick.RemoveListener (OnBackButtonClicked);
            restartButton.onClick.AddListener (OnRestartButtonClicked);
        }

        private void OnBackButtonClicked ()
        {
            SwitchToMainMenu ();
        }

        private void OnRestartButtonClicked ()
        {

        }

        private void SwitchToMainMenu ()
        {
            mainPanel.SetActive (true);
            gamePanel.SetActive (false);
        }
    }
}