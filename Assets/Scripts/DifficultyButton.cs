using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SymmetricalOctoEureka
{
    public class DifficultyButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image image;
        [SerializeField] private Button button;

        public void Initialize (DifficultyLevel difficultyLevel, Action<DifficultyLevel> onButtonClick)
        {
            name = $"Button_{difficultyLevel.name}";
            text.SetText ($"{difficultyLevel.name} ({difficultyLevel.rows}x{difficultyLevel.columns})");
            button.onClick.AddListener (() => onButtonClick?.Invoke (difficultyLevel));
        }

        private void OnDestroy ()
        {
            button.onClick.RemoveAllListeners ();
        }
    }
}