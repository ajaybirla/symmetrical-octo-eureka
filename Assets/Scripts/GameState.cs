using System;
using System.Collections.Generic;

namespace SymmetricalOctoEureka
{
    [System.Serializable]
    public class GameState
    {
        public int rows;
        public int columns;
        public int turns;
        public int matches;
        public List<int> layoutCardIds = new List<int> ();
        public List<int> matchedCardIds = new List<int> ();

        public int TotalCards => rows * columns;
        public int TotalPairs => TotalCards / 2;

        public GameState (int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            this.turns = 0;
            this.matches = 0;
            this.layoutCardIds = new List<int> ();
            this.matchedCardIds = new List<int> ();

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
            Random random = new Random ();

            int n = layoutCardIds.Count;

            for (int i = 0; i < n; i++)
            {
                int randomIndex = random.Next (i, n);

                int temp = layoutCardIds [i];
                layoutCardIds [i] = layoutCardIds [randomIndex];
                layoutCardIds [randomIndex] = temp;
            }
        }
    }
}