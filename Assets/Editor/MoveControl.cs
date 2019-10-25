using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MixOne
{
    public class MoveControl
    {
 
        [MenuItem("MixOne/TestMode")]
        static void TestMode()
        {
            CameraSystem.SwitchToTestMode();
        }

    }
}

