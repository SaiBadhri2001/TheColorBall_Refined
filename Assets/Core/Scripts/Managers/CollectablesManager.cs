using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheColorBall.Core
{
    public class CollectablesManager : MonoBehaviour
    {
        [SerializeField] bool _checkForGate = false;
        public GameObject gate;
        public List<GameObject> totalCollectablesInScene;
        public int noOfCollectedCollectables;
        void Awake()
        {
            noOfCollectedCollectables = 0;
        }
        public void CheckForRemainingCollectables(CollectablesType collectablesType)
        {
            if (_checkForGate)
                if (noOfCollectedCollectables == totalCollectablesInScene.Count)
                {
                    gate.SetActive(false);
                }
        }
    }
    public class CollectableFuntions
    {
        public Action<CollectablesType> collectables;
    }
    public enum CollectablesType
    {
        TinyDiamond,
        HugeDiamond,
        Bomb,
        BombDiamond
    }
}