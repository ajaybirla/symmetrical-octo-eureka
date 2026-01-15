using UnityEngine;
using UnityEngine.UI;

namespace SymmetricalOctoEureka
{
    public class MainMenu : MonoBehaviour
    {
        [Header ("Panels")]
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject savedGameObj;

        [Header ("Buttons")]
        [SerializeField] private Transform buttonsParent;
        [SerializeField] private Button continueButton;

        [Header ("Configuration")]
        public Configuration configuration;

        private void Start ()
        {
            DifficultyLevel [] difficultyLevels = configuration.difficultyLevels;

            foreach (DifficultyLevel difficultyLevel in difficultyLevels)
            {
                GameObject buttonObject = Instantiate (configuration.buttonPrefab, buttonsParent);
                DifficultyButton difficultyButton = buttonObject.GetComponent<DifficultyButton> ();
                difficultyButton.Initialize (difficultyLevel, OnDifficultySelected);
            }

            continueButton.onClick.AddListener (OnContinueButtonClicked);
        }

        private void OnEnable ()
        {
            bool hasSavedGame = PlayerPrefs.HasKey ("SavedGame");

            savedGameObj.SetActive (hasSavedGame);
        }

        private void OnDestroy ()
        {
            continueButton.onClick.RemoveListener (OnContinueButtonClicked);
        }

        private void OnDifficultySelected (DifficultyLevel difficultyLevel)
        {
            SwitchToGameplay ();
        }

        private void OnContinueButtonClicked ()
        {
            SwitchToGameplay ();
        }

        private void SwitchToGameplay ()
        {
            mainPanel.SetActive (false);
            gamePanel.SetActive (true);
        }
    }
}