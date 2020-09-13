using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace Kintal
{
    public class GarbageController : MonoBehaviour
    {

        private void FixedUpdate()
        {
            if (Time.frameCount % 300 == 0)
            {
                Debug.Log("[GarbageCollector] Collect");
                System.GC.Collect();
            }
        }

        static void ListenForGCModeChange()
        {
            // Listen on garbage collector mode changes.
            GarbageCollector.GCModeChanged += (GarbageCollector.Mode mode) =>
            {
                Debug.Log("GCModeChanged: " + mode);
            };
        }

        static void LogMode()
        {
            Debug.Log("GCMode: " + GarbageCollector.GCMode);
        }

        public static void EnableGC()
        {
            GarbageCollector.GCMode = GarbageCollector.Mode.Enabled;
            // Trigger a collection to free memory.
            GC.Collect();
        }

        public static void DisableGC()
        {
            GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
        }
    }
}
