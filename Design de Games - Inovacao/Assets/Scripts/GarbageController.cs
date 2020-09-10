using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace Kintal
{
    public class GarbageController : MonoBehaviour
    {

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
            FPSDisplay.gcOn = true;
            GarbageCollector.GCMode = GarbageCollector.Mode.Enabled;
            // Trigger a collection to free memory.
            GC.Collect();
            FPSDisplay.gcOn = true;
        }

        public static void DisableGC()
        {
            FPSDisplay.gcOn = false;
            GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
            FPSDisplay.gcOn = false;
        }
    }
}
