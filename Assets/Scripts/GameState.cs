using System.Collections.Generic;
using UnityEngine;

namespace SymmetricalOctoEureka
{
    [System.Serializable]
    public class GameState
    {
        public int rows;
        public int columns;
        public int turns;
        public int matches;
        public int streak;
        public int score;
        public List<int> layoutCardIds = new List<int> ();
        public List<int> matchedCardIds = new List<int> ();

        public int TotalCards => rows * columns;
        public int TotalPairs => TotalCards / 2;
        public bool IsGameCompleted => matches >= TotalPairs;
        public bool IsGameRunning => turns > 0 && !IsGameCompleted;

        private const string SAVE_KEY = "SaveData";

        public GameState (int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;

            GenerateRandomLayout ();
        }

        private void GenerateRandomLayout ()
        {
            layoutCardIds.Clear ();

            int totalPairs = TotalPairs;

            for (int i = 0; i < totalPairs; i++)
            {
                layoutCardIds.Add (i);
                layoutCardIds.Add (i);
            }

            ShuffleLayout ();
        }

        private void ShuffleLayout ()
        {
            System.Random random = new System.Random ();

            int n = layoutCardIds.Count;

            for (int i = 0; i < n; i++)
            {
                int randomIndex = random.Next (i, n);

                int temp = layoutCardIds [i];
                layoutCardIds [i] = layoutCardIds [randomIndex];
                layoutCardIds [randomIndex] = temp;
            }
        }

        public void AddMatchedCard (int cardId)
        {
            if (!matchedCardIds.Contains (cardId))
            {
                matchedCardIds.Add (cardId);
            }
        }

        public bool IsCardMatched (int cardId)
        {
            return matchedCardIds.Contains (cardId);
        }

        public static GameState Load ()
        {
            if (!PlayerPrefs.HasKey (SAVE_KEY)) return null;

            string json = PlayerPrefs.GetString (SAVE_KEY);

            return JsonUtility.FromJson<GameState> (json);
        }

        public void Save ()
        {
            string json = JsonUtility.ToJson (this);

            PlayerPrefs.SetString (SAVE_KEY, json);

            PlayerPrefs.Save ();
        }
    }
}