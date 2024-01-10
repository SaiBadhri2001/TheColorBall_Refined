using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheColorBall.Core
{
    public class ObstracleProperties : MonoBehaviour
    {
        [SerializeField] bool _rotateModule = true;
        [SerializeField] float _rotationSpeed = 1;
        public List<GameObject> ObstracleWalls;
        private GameObject _playerColorObstracle;
        [SerializeField] LevelProperties _levelProperties;
        void Awake()
        {
            SetObstracleColor();
        }

        private void SetObstracleColor()
        {
            foreach (GameObject obstracleWall in ObstracleWalls)
            {
                obstracleWall.GetComponent<SpriteRenderer>().color = GameManager.instance.GetRandomColor();
                //obstracleWall.GetComponent<SpriteRenderer>().color = Color.white;
            }
            int _playerColorObstracleIndex = GameManager.instance.GetRandomNumber(ObstracleWalls.Count);
            _playerColorObstracle = ObstracleWalls[_playerColorObstracleIndex];
            StartCoroutine(GetPreviousColor());
        }

        private IEnumerator GetPreviousColor()
        {
            yield return new WaitUntil(() => _levelProperties.WaitForPreviousColor);
            //set obstracle color
            _playerColorObstracle.GetComponent<SpriteRenderer>().color = _levelProperties._previousLevelChosenColor;
        }

        private void OnRandomColorHit(Color selectedObstracleColor)
        {
            _playerColorObstracle.GetComponent<SpriteRenderer>().color = selectedObstracleColor;
            Debug.Log("SetColor");
        }
        void Update()
        {
            if (_rotateModule && StaticDatas.isPlayerAlive)
                transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        }
    }
}