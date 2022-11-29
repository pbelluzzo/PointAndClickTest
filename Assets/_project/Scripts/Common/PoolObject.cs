using System;
using UnityEngine;

namespace PointAndClick
{
    public abstract class PoolObject : MonoBehaviour
    {
        protected Action<PoolObject> _killAction;
        public virtual void Initialize(Action<PoolObject> KillAction)
        {
            _killAction = KillAction;
        }
    }
}