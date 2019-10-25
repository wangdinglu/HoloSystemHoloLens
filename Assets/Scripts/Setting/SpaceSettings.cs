using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MixOne
{
    public class SpaceSettings : MonoBehaviour
    {
        // Start is called before the first frame update
        public static int length = 72;
        public static float cell = 360 / (float)length;
        public static float basicDynamicRadius = 220;
        public static float layerDynamicRadius = 30;

        public static float basicStaticRadius = 150;
        public static float layerStaticRadius = 20;

        //    _ip = "192.168.3.77";
        public static string serverIP = "192.168.3.72";
    }
}