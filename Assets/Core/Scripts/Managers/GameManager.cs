using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace TheColorBall.Core
{
    public class GameManager : MonoBehaviour
    {
        public GameProperties gameProperties;
        public static GameManager instance;
        public TextMeshProUGUI scoreDisplay;

        [Header("Debug")]
        public List<GameObject> currentLevelsInScreen = new List<GameObject>();
        public bool isDebug = false;
        float initialSpeed = 1.0f;
        public Color CurrentBallColor { get; private set; }

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Multiple GameManagers Found!!!");
            }
            else
            {
                instance = this;
            }
            Application.targetFrameRate = gameProperties.targetFrameRate;


            for (int i = 0; i < gameProperties.totalLevelsAtTime - 1; i++)
            {
                int index = GetRandomNumber(gameProperties.Levels.Count);
                currentLevelsInScreen.Add(Instantiate(gameProperties.Levels[index],
                                            currentLevelsInScreen[currentLevelsInScreen.Count - 1].GetComponentInChildren<LevelProperties>().endPoint.transform.position,
                                                                                                        Quaternion.identity));
            }

            StaticDatas.isPlayStarted = false;
        }
        void Start()
        {
            initialSpeed = gameProperties.initialMovementSpeed;

            ScoreManager.currentScore = 0;
            scoreDisplay.text = ScoreManager.currentScore.ToString();
        }

        public int GetRandomNumber(int maxNumber)
        {
            return UnityEngine.Random.Range(1, maxNumber);
        }

        void Update()
        {
            foreach (GameObject currentLevelInScreen in currentLevelsInScreen)
            {
                if (StaticDatas.isPlayerAlive && StaticDatas.isPlayStarted)
                {
                    //currentLevelInScreen.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -initialSpeed);
                    //currentLevelInScreen.transform.DOMoveY(currentLevelInScreen.transform.position.y - initialSpeed * Time.deltaTime, gameProperties.speedMultiplier);
                    float moveLevel = currentLevelInScreen.transform.position.y - initialSpeed * Time.deltaTime;
                    currentLevelInScreen.transform.position = new Vector3(currentLevelInScreen.transform.position.x, moveLevel, currentLevelInScreen.transform.position.z);
                }
                //else
                //currentLevelInScreen.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
        private void FixedUpdate()
        {
            if (StaticDatas.isPlayerAlive && StaticDatas.isPlayStarted)
            {
                initialSpeed = initialSpeed + gameProperties.speedMultiplier * Time.deltaTime;
                if (gameProperties.initialMovementSpeed >= gameProperties.maxMovementSpeed)
                {
                    initialSpeed = gameProperties.maxMovementSpeed;
                }
            }
        }
        public void SpawnLevel()
        {
            if (StaticDatas.totalCrosses > gameProperties.levelsRunsBelow)
            {
                int index = GetRandomNumber(gameProperties.Levels.Count);
                currentLevelsInScreen.Add(Instantiate(gameProperties.Levels[index],
                                            currentLevelsInScreen[currentLevelsInScreen.Count - 1].GetComponentInChildren<LevelProperties>().endPoint.transform.position,
                                                                                                        Quaternion.identity));
            }
        }
        public void DeformLevel()
        {
            if (StaticDatas.totalCrosses > gameProperties.levelsRunsBelow)
            {
                GameObject objectToBeRemoved = currentLevelsInScreen[0];
                currentLevelsInScreen.RemoveAt(0);
                Destroy(objectToBeRemoved);
            }
        }
        public void SetBallColor(Color color)
        {
            BallProperties.instance.PlayerSpriteRenderer.color = color;
            CurrentBallColor = BallProperties.instance.PlayerSpriteRenderer.color;
            //BallColorChanged?.Invoke();
        }
        public Color GetRandomColor()
        {
            Color color = new Color();
            if (gameProperties.totalColors.Count != 0)
            {
                int _colorIndex = GetRandomNumber(gameProperties.totalColors.Count);
                color = gameProperties.totalColors[_colorIndex];
            }
            return color;
        }
        public void SpawnLevelEditor()
        {
            int index = GetRandomNumber(gameProperties.Levels.Count);
            currentLevelsInScreen.Add(Instantiate(gameProperties.Levels[index],
                                        currentLevelsInScreen[currentLevelsInScreen.Count - 1].GetComponentInChildren<LevelProperties>().endPoint.transform.position,
                                                                                                    Quaternion.identity));
        }
        public void DeformLevelEditor()
        {
            DestroyImmediate(currentLevelsInScreen[0]);
            currentLevelsInScreen.RemoveAt(0);
        }
        public void OnClickRestartButton()
        {
            SceneManager.LoadScene(gameObject.scene.name);
        }
    }
}