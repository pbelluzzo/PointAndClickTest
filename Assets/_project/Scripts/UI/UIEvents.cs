using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PointAndClick.UI
{
    public static class UIEvents
    {
        public static event Action OnDecreaseSpeedButtonClicked;
        public static void NotifyDecreaseSpeedButtonClicked() => OnDecreaseSpeedButtonClicked?.Invoke();

        public static event Action OnIncreaseSpeedButtonClicked;
        public static void NotifyIncreaseSpeedButtonClicked() => OnIncreaseSpeedButtonClicked?.Invoke();
    }

}
