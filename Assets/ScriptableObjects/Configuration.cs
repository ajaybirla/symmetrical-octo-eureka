using UnityEngine;

namespace SymmetricalOctoEureka
{
    [CreateAssetMenu (fileName = "Configuration", menuName = "SymmetricalOctoEureka/Configuration")]
    public class Configuration : ScriptableObject
    {
        [Header ("Prefabs")]
        public GameObject buttonPrefab;
        public GameObject cardPrefab;

        [Header ("Sprites")]
        public Sprite [] cardFrontSprites;
        public Sprite cardBackSprite;

        [Header ("Audio Clips")]
        public AudioClip cardFlipSound;
        public AudioClip matchSound;
        public AudioClip mismatchSound;
        public AudioClip gameOverSound;

        [Header ("Settings")]
        [Range (1f, 5f)] public float initialPeekDuration = 2f;
        [Range (1f, 5f)] public float matchDelay = 1f;
        [Range (1f, 5f)] public float mismatchDelay = 1f;
        [Range (0f, 1f)] public float soundVolume = 0.7f;

        [Header ("Difficulty")]
        public DifficultyLevel [] difficultyLevels = new []
        {
            new DifficultyLevel { name = "Easy", rows = 3, columns = 4 },
            new DifficultyLevel { name = "Normal", rows = 4, columns = 5 },
            new DifficultyLevel { name = "Hard", rows = 4, columns = 6 },
            new DifficultyLevel { name = "Expert", rows = 5, columns = 8 },
            new DifficultyLevel { name = "Insane", rows = 6, columns = 10 }
        };
    }

    [System.Serializable]
    public class DifficultyLevel
    {
        public string name;
        public int rows;
        public int columns;
    }
}