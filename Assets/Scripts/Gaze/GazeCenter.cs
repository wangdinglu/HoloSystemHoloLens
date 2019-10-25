using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MixOne
{
    public class GazeCenter : MonoBehaviour
    {
        public Image pointer;
        public Camera gyroCamera;
        public Camera testCamera;

        private Vector3 pos = new Vector3(0, -100, 0);
        private Vector3 testCenter;
        private Vector3 gyroCenter;

        private Ray ray;
        private RaycastHit hit;
        private bool isCollider;
        private bool status;
        private Material pointerMat;

        private float width;
        private float height;

        private void Awake()
        {
            testCamera = GameObject.Find("testCamera").GetComponent<Camera>();
            gyroCamera = GameObject.Find("HoloLensCamera").GetComponent<Camera>();
            pointer = GameObject.Find("PointerImage").GetComponent<Image>();

            //pointerMat = pointer.GetComponent<MeshRenderer>().material;
            testCenter = new Vector3(testCamera.pixelWidth/2, testCamera.pixelHeight/2,0);
            gyroCenter = new Vector3(gyroCamera.pixelWidth/2, gyroCamera.pixelHeight/2,0);
            isCollider = false;
            status = false;

            width = Camera.main.pixelWidth;
            height = Camera.main.pixelHeight;
        }

        private void Update()
        {
            if (!CameraSystem.testMode)
            {
                ray = gyroCamera.ScreenPointToRay(gyroCenter);
            }
            else
            {
                ray = testCamera.ScreenPointToRay(testCenter);

            }
            //Debug.DrawLine(transform.position, transform.position + transform.forward * 100, Color.red);

            isCollider = Physics.Raycast(ray, out hit);
            if (isCollider)
            {
                pointer.color = new Color(0.2f,0.2f,1f);
            }
            else
            {
                pointer.color = new Color(1f, 1f, 0.8f);
            }
            //Debug.Log(test.moveable);
            status = isCollider;
        }

        public GameObject GetObject()
        {
            if (isCollider)
            {
                return hit.collider.gameObject;
            }
            return null;
        }

        public void MovePointer(Vector3 move)
        {
            Vector3 finalMove = move;
            if (pointer.transform.position.x + move.x > width)
            {
                finalMove.x = 0;
            }
            if (pointer.transform.position.x + move.x < 0)
            {
                finalMove.x = 0;
            }
            if (pointer.transform.position.y + move.y > height)
            {
                finalMove.y = 0;
            }
            if (pointer.transform.position.y + move.y < 0)
            {
                finalMove.y = 0;
            }

            pointer.transform.position += finalMove;
        }


    }
}