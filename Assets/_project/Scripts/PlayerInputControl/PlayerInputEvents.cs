using System;
using UnityEngine;

namespace PointAndClick.PlayerInputControl 
{
    public static class PlayerInputEvents
    {
        public static event Action<RaycastHit> OnWorldPointClickedEvent;
        public static void NotifyWorldPointClicked(RaycastHit hit) => OnWorldPointClickedEvent?.Invoke(hit);
    }

}
