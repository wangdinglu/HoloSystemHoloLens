using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MixOne
{
    public class StatusController : MonoBehaviour
    {
        
        public LayerSystem ls;
        public WindowSystem ws;
        public LensServer server;
        public CameraSystem cs;
        public GazeCenter gc;
        private bool cameraHold = false;

        private List<string> LayerTaskList = new List<string>
        {
            "Stop","Move","Left","Right"
        };

        private List<string> WindowTaskList = new List<string>
        {
            "NeteaseMusic","Tiktok","Wechat","Subscription","Blog"
        };

        //private Dictionary<string>

        public void Awake()
        {
            cs = GameObject.Find("WindowManager").GetComponent<CameraSystem>();
            ls = GameObject.Find("WindowManager").GetComponent<LayerSystem>();
            ws = GameObject.Find("WindowManager").GetComponent<WindowSystem>();
            server = GameObject.Find("ServerManager").GetComponent<LensServer>();
            gc = GameObject.Find("PointerImage").GetComponent<GazeCenter>();
        }


        public void CameraTask(string task)
        {

            if (cameraHold)
            {
                cs.EnableGyro();
                cameraHold = !cameraHold;
            }
            else
            {
                cs.StopGyro();
                cameraHold = !cameraHold;
            }

        }

        public void LayerTask(string task)
        {
            GameObject layer;
            //Debug.Log("-----");
            switch (task)
            {
                
                case "Left":
                    layer = ls.GetLayer("DynamicLayer");
                    //GameObject windows = layer.transform.ge
                    //layer.transform.Rotate(new Vector3(0, -10f, 0));
                    ws.RotateDynamicWindows(layer ,- 10);
                    //server.Send(layer.transform.eulerAngles.ToString());
                    break;
                case "Right":
                    
                    layer = ls.GetLayer("DynamicLayer");
                    ws.RotateDynamicWindows(layer,+10);
                    //layer.transform.Rotate(new Vector3(0, +10f, 0));
                    //server.Send(obj.transform.eulerAngles.ToString());
                    break;
                case "Up":

                    ws.RotateDynamicCamera( -10);
                    //layer.transform.Rotate(new Vector3(0, +10f, 0));
                    //server.Send(obj.transform.eulerAngles.ToString());
                    break;
                case "Down":

                    ws.RotateDynamicCamera(+10);
                    //layer.transform.Rotate(new Vector3(0, +10f, 0));
                    //server.Send(obj.transform.eulerAngles.ToString());
                    break;
                default:
                    break;

            }
        }

        public void TouchTask(string task)
        {
            switch (task)
            {
                case "Tap":
                    GameObject colliderOpen = gc.GetObject();
                    if (colliderOpen != null)
                    {
                        string openName = colliderOpen.name;
                        if (WindowSystem.Applist.Contains(openName) & WindowSystem.DynamicApplist.Contains(openName))
                        {
                            GameObject layer = ls.GetLayer("DynamicLayer");
                            if (ws.CheckExist(openName))
                            {
                                ws.PutDynamicWindowForward(layer, openName);
                            }
                            else
                            {
                                ws.insertDynamicWindow(layer, openName);
                            }
                        }
                        if (WindowSystem.Applist.Contains(openName) & WindowSystem.StaticApplist.Contains(openName))
                        {
                            GameObject layer = ls.GetLayer("StaticLayer");
                            if (ws.CheckExist(openName))
                            {
                                ws.PutStaticWindowForward(layer, openName);
                            }
                            else
                            {
                                ws.insertStaticWindow(layer, openName);
                            }
                        }
                        if (WindowSystem.SwitchShowList.ContainsKey(openName))
                        {
                            ws.SwitchStatus(WindowSystem.SwitchShowList[openName]);
                        }
                        if (WindowSystem.SwitchHideList.ContainsKey(openName))
                        {
                            ws.SwitchStatus(WindowSystem.SwitchHideList[openName]);
                        }
                    }
                    break;
                case "Hold":
                    GameObject colliderClose = gc.GetObject();
                    if (colliderClose != null)
                    {
                        string closeName = colliderClose.name;
                        if (ws.CheckExist(closeName))
                        {
                            Debug.Log("Closing " + closeName);
                            ws.CloseWindow(closeName);
                        }
                    }
                    break;

                default:
                    break;
            }
            
        }

        public void ClientTask(string task)
        {
            string[] taskInfo = task.Split(':');
            //Debug.Log(window.gameObject.name);
            if (taskInfo.Length > 1)
            {
                switch (taskInfo[1])
                {


                    case "DualHold":
                        CameraTask(taskInfo[1]);
                        //server.Send(obj.transform.eulerAngles.ToString());
                        break;
                    case "Left":
                        LayerTask(taskInfo[1]);
                        break;
                    case "Right":
                        LayerTask(taskInfo[1]);
                        break;
                    //server.Send(obj.transform.eulerAngles.ToString());
                    case "Tap":
                        TouchTask(taskInfo[1]);
                        break;
                    case "Hold":
                        TouchTask(taskInfo[1]);
                        break;
                    default:
                        break;

                }

                if (taskInfo[1].Contains("x/"))
                {
                    Debug.Log(taskInfo[1]);
                    string[] move = taskInfo[1].Replace("Touch", "").Split('/');
                    gc.MovePointer(new Vector3(float.Parse(move[1]), float.Parse(move[3]), 0));
                }
            }
            

        }
    }

}
