using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MixOne
{
    public class ControlList : MonoBehaviour
    {
        private static List<string> operation = new List<string>();
        private StatusController sc;
        private GameObject appLayer;
        private GameObject staticLayer;

        public LayerSystem ls;
        public WindowSystem ws;
        public Camera gyroCamera;
        public Camera testCamera;

        public void pushOperation(string op)
        {
            operation.Add(op);
            //Debug.Log("Add " + op);
        }

        public void remove(string op)
        {
            operation.Remove(op);
        }

        private void Awake()
        {
            sc = GameObject.Find("StatusControl").GetComponent<StatusController>();
            ws = GameObject.Find("WindowManager").GetComponent<WindowSystem>();
            testCamera = GameObject.Find("testCamera").GetComponent<Camera>();
            gyroCamera = GameObject.Find("HoloLensCamera").GetComponent<Camera>();
        }

        private void Start()
        {
            ls = GameObject.Find("WindowManager").GetComponent<LayerSystem>();
            appLayer = ls.GetLayer("ApplicationLayer");
            staticLayer = ls.GetLayer("StaticLayer");

            //gyroCamera = GameObject.Find("GyroCamera").GetComponent<Camera>();
        }

        private void Update()
        {
            if (operation.Count != 0)
            {
                string op = operation[0];
                sc.ClientTask(op);
                operation.Remove(op);

            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                sc.ClientTask("Touch:Open");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                sc.ClientTask("Touch:Close");
            }

            if (CameraSystem.testMode)
            {
                if(testCamera.transform.eulerAngles.x<28)
                    appLayer.transform.eulerAngles = new Vector3(0, testCamera.transform.eulerAngles.y, 0);
                staticLayer.transform.eulerAngles = testCamera.transform.eulerAngles;

            }
            else
            {
                if (gyroCamera.transform.eulerAngles.x < 28)
                    appLayer.transform.eulerAngles = new Vector3(0, gyroCamera.transform.eulerAngles.y, 0);
                //appLayer.transform.eulerAngles = new Vector3(0, gyroCamera.transform.eulerAngles.y, 0);
                staticLayer.transform.eulerAngles = gyroCamera.transform.eulerAngles;

            }


        }

        
    }
}

