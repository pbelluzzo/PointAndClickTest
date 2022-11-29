using UnityEngine;
using PointAndClick.PlayerInputControl;
using System;

namespace PointAndClick.CollectibleSystem
{
    public class Collectible : PoolObject
    {
        private Collider _collider;
        private ICollector _triggedCollector;
        private bool _collectionTriggered;

        public override void Initialize(Action<PoolObject> KillAction)
        {
            base.Initialize(KillAction);
            _triggedCollector = null;
            _collectionTriggered = false;
        }

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            PlayerInputEvents.OnWorldPointClickedEvent += CheckPlayerClick;
        }

        private void OnDisable()
        {
            PlayerInputEvents.OnWorldPointClickedEvent -= CheckPlayerClick;
        }

        private void CheckPlayerClick(RaycastHit hit) 
        {
            if (hit.collider == _collider) 
            {
                HandleCollection();
                return;
            }

            _collectionTriggered = false;
        }

        private void HandleCollection() 
        {
            _collectionTriggered = true;

            if (_triggedCollector == null) 
            {
                return;
            }

            Collect();
        }

        private void OnTriggerEnter(Collider other)
        {
            ICollector collector;
            other.TryGetComponent(out collector);

            if (collector == null)
            {
                return;
            }

            _triggedCollector = collector;

            if (_collectionTriggered) 
            {
                Collect();
                return;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            ICollector collector;
            other.TryGetComponent(out collector);

            if (collector == null)
            {
                return;
            }

            if (_triggedCollector == collector) 
            {
                _triggedCollector = null;
            }
        }

        private void Collect()
        {
            CollectibleEvents.NotifyCollectibleCollected(this);
            _killAction(this);
        }
    }
}
