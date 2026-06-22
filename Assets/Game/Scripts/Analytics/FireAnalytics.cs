using Firebase;
using Firebase.Analytics;
using Unity.VisualScripting;
using UnityEngine;

namespace Analytics
{
    public class FireAnalytics : IAnalytics, IInitializable
    {
        public void Initialize()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            });
        }

        public void LogGameStart()
        {
            FirebaseAnalytics.LogEvent("start_game");
        }

        public void LogGameEnd(int gunShotCount, int laserShotCount, int enemiesDestroyedCount)
        {
            FirebaseAnalytics.LogEvent("end_game",
                new Parameter("gun_shot_count", gunShotCount),
                new Parameter("laser_shot_count", laserShotCount),
                new Parameter("enemies_destroyed_count", enemiesDestroyedCount));
        }

        public void LogLaserShot()
        {
            FirebaseAnalytics.LogEvent("laser_shot");
        }
    }
}