using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System.Linq;

namespace PointAndClick.CollectibleSystem
{
    public class CollectiblesSpawner : MonoBehaviour
    {
        [SerializeField] private PoolObject _collectiblePrefab;
        [SerializeField] private float _spawnTime;
        [SerializeField] private List<CollectibleSpawnPosition> _spawnPositions = new List<CollectibleSpawnPosition>();

        private List<CollectibleSpawnPosition> _usedSpawnPositions = new List<CollectibleSpawnPosition>();
        private ObjectPool<PoolObject> _collectiblePool;
        private WaitForSeconds _waitSpawnTime;

        private void Awake()
        {
            _waitSpawnTime = new WaitForSeconds(_spawnTime);
            _collectiblePool = new(CreateNewCollectible, OnSpawnFromPool, OnReturnToPool, OnDestroyFromPool);
        }

        private void OnEnable()
        {
            GameEvents.OnStartGameEvent += StartSpawnCycle;
        }

        private void OnDisable()
        {
            GameEvents.OnStartGameEvent -= StartSpawnCycle;
        }

        private void StartSpawnCycle()
        {
            StartCoroutine(SpawnCollectibles());
        }

        private IEnumerator SpawnCollectibles()
        {
            while (true)
            {
                if (_spawnPositions.Count > 0) 
                {
                    _collectiblePool.Get();
                }

                yield return _waitSpawnTime;
            }
        }

        private CollectibleSpawnPosition GetRandomSpawnPosition()
        {
            return _spawnPositions[Random.Range(0, _spawnPositions.Count)];
        }

        private void UseSpawnPosition(CollectibleSpawnPosition spawnPosition, GameObject gameObj) 
        {
            spawnPosition.ActiveCollectible = gameObj;
            _spawnPositions.Remove(spawnPosition);
            _usedSpawnPositions.Add(spawnPosition);
        }

        private void ClearSpawnPosition(GameObject gameObj)
        {
            CollectibleSpawnPosition spawnPos = _usedSpawnPositions.Where(s => s.ActiveCollectible == gameObj).First();
            _usedSpawnPositions.Remove(spawnPos);
            _spawnPositions.Add(spawnPos);
            spawnPos.ActiveCollectible = null;
        }

        private void OnCollectibleDie(PoolObject poolObject)
        {
            _collectiblePool.Release(poolObject);
        }

        private PoolObject CreateNewCollectible()
        {
            return Instantiate(_collectiblePrefab);
        }

        private void OnSpawnFromPool(PoolObject poolObject)
        {
            CollectibleSpawnPosition spawnPosition = GetRandomSpawnPosition();
            UseSpawnPosition(spawnPosition, poolObject.gameObject);
            poolObject.transform.position = spawnPosition.transform.position;
            poolObject.gameObject.SetActive(true);
            poolObject.Initialize(OnCollectibleDie);
        }

        private void OnReturnToPool(PoolObject poolObject)
        {
            ClearSpawnPosition(poolObject.gameObject);
            poolObject.gameObject.SetActive(false);
        }

        private void OnDestroyFromPool(PoolObject poolObject)
        {
            Destroy(poolObject.gameObject);
        }
    }
}