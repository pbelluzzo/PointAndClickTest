using System;

namespace PointAndClick.CollectibleSystem 
{
    public static class CollectibleEvents
    {
        public static event Action<Collectible> OnCollectibleCollectedEvent;
        public static void NotifyCollectibleCollected(Collectible collectible) => OnCollectibleCollectedEvent?.Invoke(collectible);
    }

}
