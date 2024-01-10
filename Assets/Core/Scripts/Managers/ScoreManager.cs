using Essentails;
using UnityEngine;

namespace TheColorBall.Core
{
    public class ScoreManager
    {
        public static int currentScore;
        public static int HighScore;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void InitScoreManager()
        {
            HighScore = int.Parse(FileSystem.ReadFromTextFile("ColorBallSaveFile", Application.persistentDataPath));
        }
        public static void HighScoreCalculator()
        {
            if (currentScore > HighScore)
            {
                HighScore = currentScore;

                FileSystem.CheckIfTextFileExistThenWrite("ColorBallSaveFile", Application.persistentDataPath, HighScore.ToString());
                /* PlayerPrefs.SetInt("HighScore", HighScore);
                PlayerPrefs.Save(); */
            }
        }
        public static void UpdateCurrentScore(CollectablesType collectableType)
        {
            switch (collectableType)
            {
                case CollectablesType.TinyDiamond:
                    {
                        currentScore += 100;
                        break;
                    }
                case CollectablesType.HugeDiamond:
                    {
                        currentScore += 300;
                        break;
                    }
                case CollectablesType.Bomb:
                    {
                        currentScore -= 500;
                        break;
                    }
                case CollectablesType.BombDiamond:
                    {
                        currentScore += 100;
                        break;
                    }
            }
        }
        public static void DisplayCurrentScore(CollectablesType collectablesType)
        {
            GameManager.instance.scoreDisplay.text = currentScore.ToString();
        }
    }
}