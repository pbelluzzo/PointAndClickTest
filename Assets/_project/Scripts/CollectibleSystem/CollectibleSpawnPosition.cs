using UnityEngine;

namespace PointAndClick.CollectibleSystem
{
    public class CollectibleSpawnPosition : MonoBehaviour
    {
        private GameObject _activeObject;

        public GameObject ActiveCollectible { get => _activeObject; set => _activeObject = value; }

    }
}