using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MixOne
{
    public class WindowSystem : MonoBehaviour
    {

        

        public Dictionary<string, GameObject> Windows;
        //public static List<SpaceBase> Space = new List<SpaceBase>();
        //public static List<GameObject> WindowObject = new List<GameObject>();
        private List<SpaceBase> DynamicSpaceList = new List<SpaceBase>();
        private List<SpaceBase> StaticSpaceList = new List<SpaceBase>();

        public SpaceCalculation sc;
        public GameObject window;

        public Camera testCamera;
        public Camera gyroCamera;

        private List<GameObject> DynamicWindowObject;
        private List<GameObject> StaticWindowObject;

        private bool aiming;
        private float lerpValue = 0;
        private float movementTime = 5;

        //private GyroCameraControl gcc;


        public static Dictionary<string, WindowBase> WindowInfoList = new Dictionary<string, WindowBase>(){
            { "NeteaseMusic",new WindowBase("NeteaseMusic",4,3,1)},
            { "Xuetangx",new WindowBase("Xuetangx",8,3,2)},
            { "Tonghuashun",new WindowBase("Tonghuashun",8,3,3)},
            { "Wechat",new WindowBase("Wechat",8,3,4)},
            { "Tiktok",new WindowBase("Tiktok",6,3,5)},
            { "Baidu",new WindowBase("Baidu",6,3,6)},
            { "Kuaikan",new WindowBase("Kuaikan",3,3,7)},
            { "Lexue",new WindowBase("Lexue",2,3,8)},
            { "Migu",new WindowBase("Migu",3,3,9)},
            { "Huya",new WindowBase("Huya",6,3,10)},
            { "Iqiyi",new WindowBase("Iqiyi",6,3,11)},
            { "Taobao",new WindowBase("Taobao",6,3,12)},
        };

        public static Dictionary<int, string> IdList = new Dictionary<int, string>(){
            { 1,"NeteaseMusic"},
            { 2,"Xuetangx"},
            { 3,"Tonghuashun"},
            { 4,"Wechat"},
            { 5,"Tiktok"},
            { 6,"Baidu"},
            { 7,"Kuaikan"},
            { 8,"Lexue"},
            { 9,"Migu"},
            { 10,"Huya"},
            { 11,"Iqiyi"},
            { 12,"Taobao"},
        };

        public static List<string> Applist = new List<string>(){
            "Wechat","Xuetangx","Tonghuashun",
            "Baidu","Kuaikan","Lexue","Migu","Huya",
            "Iqiyi","Taobao","NeteaseMusic","Tiktok"
        };

        public static List<string> DynamicApplist = new List<string>(){
            "Wechat","Xuetangx","Tonghuashun",
            "Baidu","Kuaikan","Migu","Huya",
            "Iqiyi","Taobao","NeteaseMusic","Tiktok"
        };

        public static List<string> StaticApplist = new List<string>(){
            "Lexue"
        };

        public static List<string> Hidelist2 = new List<string>()
        {
            "Kuaikan_Comic1","Kuaikan_FirstLayer","Baidu_FirstLayer"


        };

        public static List<string> Hidelist = new List<string>()
        {
            
            "Baidu_SecondLayer","Baidu_Baike","Baidu_Home","Baidu_News",
            "Taobao_LiveList","Taobao_Live","Tiktok_Video1",
            "Kuaikan_Comic2","Kuaikan_Comic3","Kuaikan_Comic4",
            "Kuaikan_Comic5","Kuaikan_Comic6","Kuaikan_Comic7","Kuaikan_Comic8","Kuaikan_SecondLayer",
            "Wechat_FileTransfer","Wechat_Payment","Wechat_Friend",
            "Wechat_Group","Wechat_Underground","NeteaseMusic_Music1",
            "NeteaseMusic_Music2","NeteaseMusic_Music3","NeteaseMusic_Music4","NeteaseMusic_Music5","NeteaseMusic_Music",
            "Tiktok_Video2","Tiktok_Video3" ,"Tiktok_Video4"

        };
        


        public static Dictionary<string, string> SwitchShowList = new Dictionary<string, string>() {
            { "Baidu_Search","Baidu_SecondLayer" },
            { "Baidu_ToNews","Baidu_News" },
            { "Baidu_ToBaike","Baidu_Baike" },
            { "Baidu_ToHome","Baidu_Home" },
            { "Taobao_Icon","Taobao_LiveList" },
            { "Taobao_LiveList","Taobao_Live" },
            { "Tiktok_VideoList","Tiktok_Video1" },
            { "Kuaikan_ToComic","Kuaikan_SecondLayer" },
            { "Kuaikan_Comic1","Kuaikan_Comic2" },
            { "Kuaikan_Comic2","Kuaikan_Comic3" },
            { "Kuaikan_Comic3","Kuaikan_Comic4" },
            { "Kuaikan_Comic4","Kuaikan_Comic5" },
            { "Kuaikan_Comic5","Kuaikan_Comic6" },
            { "Kuaikan_Comic6","Kuaikan_Comic7" },
            { "Kuaikan_Comic7","Kuaikan_Comic8" },
            { "NeteaseMusic_Panel","NeteaseMusic_Music" },
            { "Wechat_ToFileTransfer","Wechat_FileTransfer" },
            { "Wechat_ToPayment","Wechat_Payment" },
            { "Wechat_ToFriend","Wechat_Friend" },
            { "Wechat_ToGroup","Wechat_Group" },
            { "NeteaseMusic_Song1","NeteaseMusic_Music1" },
            { "NeteaseMusic_Song2","NeteaseMusic_Music2" },
            { "NeteaseMusic_Song3","NeteaseMusic_Music3" },
            { "NeteaseMusic_Song4","NeteaseMusic_Music4" },
            { "NeteaseMusic_Song5","NeteaseMusic_Music5" },
            { "Tiktok_Live","Tiktok_Video2" },
            { "Tiktok_Video2","Tiktok_Video3" },
            { "Tiktok_Video3","Tiktok_Video4" },

        };

        public static Dictionary<string, string> SwitchHideList = new Dictionary<string, string>() {
            { "Baidu_ToBaike","Baidu_Home" },
            //{ "Baidu_ToHome","Baidu_News" },
            { "Baidu_ToNews","Baidu_Baike" },

            { "Kuaikan_ToComic","Kuaikan_FirstLayer" },
            { "Kuaikan_Comic1","Kuaikan_Comic1" },
            { "Kuaikan_Comic2","Kuaikan_Comic2" },
            { "Kuaikan_Comic3","Kuaikan_Comic3" },
            { "Kuaikan_Comic4","Kuaikan_Comic4" },
            { "Kuaikan_Comic5","Kuaikan_Comic5" },
            { "Kuaikan_Comic6","Kuaikan_Comic6" },
            { "Kuaikan_Comic7","Kuaikan_Comic7" },
            { "Baidu_Search","Baidu_FirstLayer" },

        };

        public void SwitchStatus(string name)
        {
            Windows[name].SetActive(!Windows[name].activeSelf);
        }

        void Awake()
        {
            Windows = new Dictionary<string, GameObject>();
            DynamicSpaceList.Add(new SpaceBase());
            StaticSpaceList.Add(new SpaceBase());

            sc = GameObject.Find("WindowManager").GetComponent<SpaceCalculation>();
            DynamicWindowObject = new List<GameObject>();
            StaticWindowObject = new List<GameObject>();
            //gcc = GameObject.Find("GYROCamera").GetComponent<GyroCameraControl>();
            testCamera = GameObject.Find("testCamera").GetComponent<Camera>();
            gyroCamera = GameObject.Find("HoloLensCamera").GetComponent<Camera>();


            foreach (string windowName in Hidelist2)
            {
                GameObject item = GameObject.Find(windowName);
                Windows.Add(windowName, item);
                //Debug.Log(item.name);
            }

            foreach (string windowName in Hidelist)
            {
                GameObject item = GameObject.Find(windowName);
                Windows.Add(windowName, item);
                //Debug.Log(item.name);
                item.SetActive(false);

            }
            
            foreach (string windowName in DynamicApplist)
            {
                GameObject item = GameObject.Find("Layers/DynamicLayer/" + windowName);
                Windows.Add(windowName,item);
                item.SetActive(false);
                //Debug.Log(item.name);
            }
            foreach (string windowName in StaticApplist)
            {
                GameObject item = GameObject.Find("Layers/StaticLayer/" + windowName);
                Windows.Add(windowName, item);
                item.SetActive(false);               
                //Debug.Log(item.name);
            }
            aiming = false;

        }

        

        public string GetWindowName(int id)
        {
            return IdList[id];
        }

        public WindowBase GetWindowInfo(string windowName)
        {
            return WindowInfoList[windowName];
        }

        public GameObject GetWindowObject(string windowName)
        {
            Debug.Log("try to get window: " + windowName);
            window = Windows[windowName];
            return window;
        }

        public GameObject GetWindowObject(int id)
        {
            //Debug.Log("try to get id: " + id.ToString());
            window = Windows[IdList[id]];
            return window;
        }

        public List<GameObject> GetObjectList(int[] moveId)
        {
            List<int> IdList = new List<int>();
            int item = 0;
            for (int i = 0; i < moveId.Length; i++)
            {
                item = moveId[i];
                if (!IdList.Exists(t => t == item) && (item != 0))
                {
                    IdList.Add(item);
                }
            }
            List<GameObject> ObjectList = new List<GameObject>();
            foreach (int id in IdList)
            {
                ObjectList.Add(GetWindowObject(id));
                //Debug.Log("Get object move --------- " + ws.GetWindowName(id));
            }
            return ObjectList;
        }


        public void insertDynamicWindow(GameObject layer,string windowName)
        {

            GameObject window = GetWindowObject(windowName);
            WindowBase windowInfo = GetWindowInfo(windowName);

            window.SetActive(true);
            //Add window to list

            DynamicWindowObject.Add(window);
            
            Vector3 checkView;
            Vector3 positionView;
            //Vector3 view = gcc.GetGyroStatus();
            if (CameraSystem.testMode)
            {
                
                checkView = testCamera.transform.eulerAngles - layer.transform.eulerAngles;
                positionView = testCamera.transform.eulerAngles;
            }
            else
            {
                checkView = gyroCamera.transform.eulerAngles - layer.transform.eulerAngles;
                positionView = gyroCamera.transform.eulerAngles;
            }

            int[] bounds = sc.GetBounds(checkView, (int)windowInfo.Width);

            int begin = bounds[0];
            int end = bounds[1];
            int layerIndex = 0;


            bool isEmpty = DynamicSpaceList[layerIndex].CheckWindowEmpty(begin, end);
            Debug.Log(window.name + " isempty:" + isEmpty.ToString() + " layerIndex: " + layerIndex.ToString() + " begin: " + begin + " end: " + end + " id: " + windowInfo.Id.ToString());
            //move windows next layer
            if (!isEmpty)
            {
                //bound of layer0
                int[] tempBound = DynamicSpaceList[layerIndex].CheckMoveBoundary(begin, end);
                int tempBegin = tempBound[0];
                int tempEnd = tempBound[1];
                //moveid of layer0
                int[] moveId = DynamicSpaceList[layerIndex].GetWindowId(tempBegin, tempEnd);
                int[] insertId = moveId;
                //insert new window to layer 0
                //DynamicSpaceList[0].SetWindowId(tempBegin, tempEnd, 0);
                DynamicSpaceList[0].RemoveWindowId(tempBegin, tempEnd);
                DynamicSpaceList[0].SetWindowId(begin, end, windowInfo.Id);

                window.transform.position = sc.SetDynamicPosition(positionView, 0);
                window.transform.eulerAngles = sc.SetRotation(positionView);

                List<GameObject> insertObject = new List<GameObject>();
                insertObject = GetObjectList(insertId);

                for (; !isEmpty; layerIndex++)
                {
                    //Debug.Log(DynamicSpaceList.Count);
                    if (DynamicSpaceList.Count >= layerIndex + 1)
                    {
                        DynamicSpaceList.Add(new SpaceBase());
                        //Debug.Log("add new layer: " + (DynamicSpaceList.Count  ).ToString());
                    }
                    //id and objects occupied by last layer

                    insertId = moveId;
                    insertObject = GetObjectList(insertId);


                    //get id to be moved in next layer
                    tempBound = DynamicSpaceList[layerIndex + 1].CheckMoveBoundary(begin, end);
                    tempBegin = tempBound[0];
                    tempEnd = tempBound[1];
                    moveId = DynamicSpaceList[layerIndex + 1].GetWindowId(tempBegin, tempEnd);

                    //move id and objects to next layer
                    //DynamicSpaceList[layerIndex + 1].SetWindowId(tempBegin, tempEnd, 0);
                    DynamicSpaceList[layerIndex + 1].RemoveWindowId(tempBegin, tempEnd);
                    DynamicSpaceList[layerIndex + 1].SetWindowId(begin, end, insertId);
                    foreach (GameObject obj in insertObject)
                    {
                        obj.transform.localScale = sc.SetDynamicScale(layerIndex + 1);
                        obj.transform.position = sc.SetDynamicPosition(obj.transform.position);
                        //Debug.Log("ScaleObject: " + obj.name);
                        //Debug.Log("layerIndex: " + layerIndex.ToString() + " begin: " + begin + " end: " + end + " id: " + windowInfo.Id.ToString());

                    }

                    //check if next layer need to be moved
                    isEmpty = sc.CheckInsert(moveId);
                    //update boundary
                    begin = tempBegin;
                    end = tempEnd;
                }

            }
            else
            {
                DynamicSpaceList[layerIndex].SetWindowId(begin, end, windowInfo.Id);

                window.transform.position = sc.SetDynamicPosition(positionView, 0);

                window.transform.eulerAngles = sc.SetRotation(positionView);

            }

        }

        public void insertStaticWindow(GameObject layer, string windowName)
        {

            GameObject window = GetWindowObject(windowName);
            WindowBase windowInfo = GetWindowInfo(windowName);

            window.SetActive(true);
            //Add window to list

            StaticWindowObject.Add(window);

            Vector3 checkView;
            //Vector3 view = gcc.GetGyroStatus();
            if (CameraSystem.testMode)
            {

                checkView = testCamera.transform.eulerAngles - layer.transform.eulerAngles;
            }
            else
            {
                checkView = gyroCamera.transform.eulerAngles - layer.transform.eulerAngles;
            }

            int[] bounds = sc.GetBounds(checkView, (int)windowInfo.Width);

            int begin = bounds[0];
            int end = bounds[1];
            int layerIndex = 0;


            bool isEmpty = StaticSpaceList[layerIndex].CheckWindowEmpty(begin, end);
            Debug.Log(window.name + " isempty:" + isEmpty.ToString() + " layerIndex: " + layerIndex.ToString() + " begin: " + begin + " end: " + end + " id: " + windowInfo.Id.ToString());
            //move windows next layer
            if (!isEmpty)
            {
                //bound of layer0
                int[] tempBound = StaticSpaceList[layerIndex].CheckMoveBoundary(begin, end);
                int tempBegin = tempBound[0];
                int tempEnd = tempBound[1];
                //moveid of layer0
                int[] moveId = StaticSpaceList[layerIndex].GetWindowId(tempBegin, tempEnd);
                int[] insertId = moveId;
                //insert new window to layer 0
                //StaticSpaceList[0].SetWindowId(tempBegin, tempEnd, 0);
                StaticSpaceList[0].RemoveWindowId(tempBegin, tempEnd);
                StaticSpaceList[0].SetWindowId(begin, end, windowInfo.Id);

                window.transform.localPosition = sc.SetStaticPosition(0);

                List<GameObject> insertObject = new List<GameObject>();
                insertObject = GetObjectList(insertId);

                for (; !isEmpty; layerIndex++)
                {
                    //Debug.Log(StaticSpaceList.Count);
                    if (StaticSpaceList.Count >= layerIndex + 1)
                    {
                        StaticSpaceList.Add(new SpaceBase());
                        //Debug.Log("add new layer: " + (StaticSpaceList.Count  ).ToString());
                    }
                    //id and objects occupied by last layer

                    insertId = moveId;
                    insertObject = GetObjectList(insertId);


                    //get id to be moved in next layer
                    tempBound = StaticSpaceList[layerIndex + 1].CheckMoveBoundary(begin, end);
                    tempBegin = tempBound[0];
                    tempEnd = tempBound[1];
                    moveId = StaticSpaceList[layerIndex + 1].GetWindowId(tempBegin, tempEnd);

                    //move id and objects to next layer
                    //StaticSpaceList[layerIndex + 1].SetWindowId(tempBegin, tempEnd, 0);
                    StaticSpaceList[layerIndex + 1].RemoveWindowId(tempBegin, tempEnd);
                    StaticSpaceList[layerIndex + 1].SetWindowId(begin, end, insertId);
                    foreach (GameObject obj in insertObject)
                    {
                        obj.transform.localScale = sc.SetStaticScale(layerIndex + 1);
                        obj.transform.position = sc.SetStaticPosition(obj.transform.position);
                        //Debug.Log("ScaleObject: " + obj.name);
                        //Debug.Log("layerIndex: " + layerIndex.ToString() + " begin: " + begin + " end: " + end + " id: " + windowInfo.Id.ToString());

                    }

                    //check if next layer need to be moved
                    isEmpty = sc.CheckInsert(moveId);
                    //update boundary
                    begin = tempBegin;
                    end = tempEnd;
                }

            }
            else
            {
                StaticSpaceList[layerIndex].SetWindowId(begin, end, windowInfo.Id);

                window.transform.localPosition = sc.SetStaticPosition(0);

            }

        }

        public void ActiveWindow(GameObject layer, string windowName)
        {
            GameObject window = GetWindowObject(windowName);
            WindowBase windowInfo = GetWindowInfo(windowName);
        }

        public void RotateDynamicWindows(GameObject layer,float angle)
        {
            float time = 0.5f;
            aiming = true;
            StartCoroutine(RotateWindow(layer, angle / time * Time.deltaTime, time));
            StopCoroutine(RotateWindow(layer, angle / time * Time.deltaTime, time));
        }
        
        IEnumerator RotateWindow(GameObject window, float angle, float time)
        {
            for (float timer = time; timer >= 0; timer -= Time.deltaTime)
            {
                window.transform.RotateAround(Vector3.zero,Vector3.up,angle);
                yield return 0;
            }
            //Debug.Log("Finish rotation");
        }

        public void RotateDynamicCamera(float angle)
        {
            float time = 0.5f;
            aiming = true;
            StartCoroutine(RotateCameraVertical(angle / time * Time.deltaTime, time));
            StopCoroutine(RotateCameraVertical(angle / time * Time.deltaTime, time));
        }

        IEnumerator RotateCameraVertical(float angle, float time)
        {
            GameObject cam = Camera.main.gameObject;
            for (float timer = time; timer >= 0; timer -= Time.deltaTime)
            {
                cam.transform.RotateAround(Vector3.zero, Vector3.right, angle);
                yield return 0;
            }
            //Debug.Log("Finish rotation");
        }

        public bool CheckExist(string name)
        {
            int id = WindowInfoList[name].Id;
            foreach(SpaceBase s in DynamicSpaceList)
            {
                if (s.WindowExist(id))
                {
                    return true; 
                }
            }
            foreach (SpaceBase s in StaticSpaceList)
            {
                if (s.WindowExist(id))
                {
                    return true;
                }
            }
            return false;
        }

        public void PutDynamicWindowForward(GameObject layer,string name)
        {
            int id = WindowInfoList[name].Id;
            int layerId = 0;
            for(int i = 0; i < DynamicSpaceList.Count; i++)
            {
                if (DynamicSpaceList[i].WindowExist(id))
                {
                    layerId = i;
                }
            }

            int[] bound = DynamicSpaceList[layerId].WindowPosition(id);
            DynamicSpaceList[layerId].RemoveWindowId(bound[0], bound[1]);
            insertDynamicWindow(layer, name);
            if (layerId != 0)
            {
                if (DynamicSpaceList[layerId].CheckSpaceEmpty())
                {
                    DynamicSpaceList.RemoveAt(layerId);
                    Debug.Log("remove layer  " + name);
                }
            }
                    
        }

        public void PutStaticWindowForward(GameObject layer, string name)
        {
            int id = WindowInfoList[name].Id;
            int layerId = 0;
            for (int i = 0; i < StaticSpaceList.Count; i++)
            {
                if (StaticSpaceList[i].WindowExist(id))
                {
                    layerId = i;
                }
            }

            int[] bound = StaticSpaceList[layerId].WindowPosition(id);
            StaticSpaceList[layerId].RemoveWindowId(bound[0], bound[1]);
            insertStaticWindow(layer, name);
            if (layerId != 0)
            {
                if (StaticSpaceList[layerId].CheckSpaceEmpty())
                {
                    StaticSpaceList.RemoveAt(layerId);
                    Debug.Log("remove layer  " + name);
                }
            }

        }

        public void CloseWindow(string name)
        {
            int id = WindowInfoList[name].Id;
            int layerId = 0;
            for (int i = 0; i < DynamicSpaceList.Count; i++)
            {
                if (DynamicSpaceList[i].WindowExist(id))
                {
                    layerId = i;
                    int[] bound = DynamicSpaceList[layerId].WindowPosition(id);
                    DynamicSpaceList[layerId].RemoveWindowId(bound[0], bound[1]);
                    if (layerId != 0)
                    {
                        if (DynamicSpaceList[layerId].CheckSpaceEmpty())
                        {
                            DynamicSpaceList.RemoveAt(layerId);
                            Debug.Log("remove layer  " + name);
                        }
                    }
                }
            }
            for (int i = 0; i < StaticSpaceList.Count; i++)
            {
                if (StaticSpaceList[i].WindowExist(id))
                {
                    layerId = i;
                    int[] bound = StaticSpaceList[layerId].WindowPosition(id);
                    StaticSpaceList[layerId].RemoveWindowId(bound[0], bound[1]);
                    if (layerId != 0)
                    {
                        if (StaticSpaceList[layerId].CheckSpaceEmpty())
                        {
                            StaticSpaceList.RemoveAt(layerId);
                            Debug.Log("remove layer  " + name);
                        }
                    }
                }
            }

            Windows[name].SetActive(false);
            
        }
    }

}

