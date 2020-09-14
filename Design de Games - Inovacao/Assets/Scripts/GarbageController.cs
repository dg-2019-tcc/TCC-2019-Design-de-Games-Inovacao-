using System;
using UnityEngine;
using UnityEngine.Scripting;


namespace Kintal
{
    public class GarbageController : MonoBehaviour
    {
        public static int callIndex;
        public static bool callFreeMemory;
        public int maxCalls = 10;
        float collectTime;



        /*private void Update()
        {
            if (callFreeMemory) { FreeMemory(); return; }

#if UNITY_EDITOR
#else
            if (Time.frameCount % 300 == 0)
            {
                Debug.Log("[Garbage Controller] frameCount");
                EnableGC();
            }
            else { DisableGC(); }
#endif
           /* if (callIndex < maxCalls) { return; }
            FreeMemory();
            Debug.Log("[Garbage Controller] Max Calls " + callIndex);*/
       /* }

        public void FreeMemory()
        {
            if (collectTime > 3f) { callIndex = 0; callFreeMemory = false; return; }
#if UNITY_EDITOR
            GC.Collect();   
#else
            EnableGC();
#endif
            collectTime += Time.deltaTime;
            Debug.Log("[Garbage Controller] FreeMemory");
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
        }*/
    }
}
