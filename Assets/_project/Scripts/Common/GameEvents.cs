using System;

namespace PointAndClick 
{
    public static class GameEvents
    {
        public static event Action OnStartGameEvent;
        public static void NotifyStartGame() => OnStartGameEvent?.Invoke();


        public static event Action<int> OnScoreUpdateEvent;
        public static void NotifyScoreUpdate(int points) => OnScoreUpdateEvent?.Invoke(points);
    }

}
