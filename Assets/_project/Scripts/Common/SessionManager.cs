using UnityEngine;
using PointAndClick.CollectibleSystem;

namespace PointAndClick 
{
    public class SessionManager : MonoBehaviour
    {
        private int _collectiblesCollected;

        private void OnEnable()
        {
            CollectibleEvents.OnCollectibleCollectedEvent += AddScore;
        }

        private void OnDisable()
        {
            CollectibleEvents.OnCollectibleCollectedEvent -= AddScore;
        }

        private void Start()
        {
            GameEvents.NotifyStartGame();
        }

        private void AddScore(Collectible collectible) 
        {
            _collectiblesCollected ++;
            GameEvents.NotifyScoreUpdate(_collectiblesCollected);
        }
    }
}
