using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MixOne
{
    public class CameraSystem : MonoBehaviour
    {
        //public static GyroCameraControl gcc;
        private GameObject layer;
        public static GameObject testCamera;
        public static GameObject gyroCamera;
        public static bool testMode = false;

        public void Awake()
        {
            //gcc = GameObject.Find("GYROCamera").GetComponent<GyroCameraControl>();
            testCamera = GameObject.Find("testCamera");
            gyroCamera = GameObject.Find("HoloLensCamera");
        }
        // Start is called before the first frame update
        public void StopGyro()
        {
            //gcc.DisableRotation();
        }

        public void EnableGyro()
        {
            //gcc.EnableRotation();
            //System.Threading.Thread.Sleep(100);
            //layer = ls.GetLayer("DynamicLayer");
            //layer.transform.eulerAngles = new Vector3(0, gcc.GetGyroStatus().y, 0);
        }

        public static void SwitchToTestMode()
        {
            gyroCamera.SetActive(false);
            testCamera.SetActive(true);
            testMode = true;

        }
    }

}
