using MoreMountains.Feedbacks;
using UnityEngine;

namespace TheColorBall.Core
{
    public class Collectables : MonoBehaviour, ICollectables
    {
        public CollectablesManager collectablesManager;
        CollectableFuntions collectableFuntions = new();
        public CollectablesType currentCollectablesType;
        public MMF_Player CollectablesFeedbacks;
        private void OnEnable()
        {
            collectablesManager = GetComponentInParent<CollectablesManager>();
            collectableFuntions.collectables += ScoreManager.UpdateCurrentScore;
            collectableFuntions.collectables += collectablesManager.CheckForRemainingCollectables;
            collectableFuntions.collectables += ScoreManager.DisplayCurrentScore;
        }
        private void OnDisable()
        {
            collectableFuntions.collectables -= ScoreManager.UpdateCurrentScore;
            collectableFuntions.collectables -= collectablesManager.CheckForRemainingCollectables;
            collectableFuntions.collectables -= ScoreManager.DisplayCurrentScore;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            OnCollected(other);
        }

        public void OnCollected(Collider2D other)
        {
            if (other.gameObject == BallProperties.instance.playerHolder.gameObject)
            {
                collectablesManager.noOfCollectedCollectables += 1;
                collectableFuntions.collectables?.Invoke(currentCollectablesType);
                CollectablesFeedbacks.PlayFeedbacks();
                Destroy(gameObject);
            }
        }
    }
}
