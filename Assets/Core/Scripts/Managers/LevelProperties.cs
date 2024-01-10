using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace TheColorBall.Core
{
    public class LevelProperties : MonoBehaviour
    {
        public Transform endPoint;
        public Transform currentPoint;
        [SerializeField] VisualEffect _colorSpark;
        public Color CurrentSparkColor;
        public Color _previousLevelChosenColor;
        public bool isCrossed;
        public bool WaitForPreviousColor = false;

        private void Start()
        {
            CurrentSparkColor = GameManager.instance.GetRandomColor();
            _colorSpark.SetVector4("MainColor", CurrentSparkColor * GameManager.instance.gameProperties._intensity);
            StartCoroutine(SetPreviousColor());
        }
        public IEnumerator SetPreviousColor()
        {
            int index = GameManager.instance.currentLevelsInScreen.IndexOf(this.gameObject.transform.root.gameObject);
            yield return new WaitUntil(() => CurrentSparkColor.a > 0);
            if (index > 0)
            {
                this._previousLevelChosenColor = GameManager.instance.currentLevelsInScreen[index - 1]
                                                        .GetComponentInChildren<LevelProperties>().CurrentSparkColor;
            }
            WaitForPreviousColor = true;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == BallProperties.instance.playerHolder)
            {
                isCrossed = true;
                StaticDatas.totalCrosses += 1;
                this.gameObject.SetActive(false);
                GameManager.instance.SetBallColor(CurrentSparkColor);
                //Debug.Log(StaticDatas.totalCrosses);
                GameManager.instance.SpawnLevel();
                GameManager.instance.DeformLevel();
            }
        }
    }
}
