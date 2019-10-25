using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MixOne
{
    public class SpaceBase : SpaceSettings
    {

        private static SpaceBase instance = null;
        public int[] block;
        public Dictionary<int,int[]> windowList;

        public SpaceBase() {
            block = new int[length];
            for (int i = 0; i < length; i++)
            {
                block[i] = 0;
            }
            windowList = new Dictionary<int, int[]>();
        }

        public SpaceBase(int begin, int end, int id)
        {
            block = new int[length];
            if (begin < end)
            {
                for (int i = 0; i < length; i++)
                {
                    if (i >= begin && i <= end)
                    {
                        block[i] = id;
                    }
                    else
                    {
                        block[i] = 0;
                    }
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    if (i <= end || i >= begin)
                    {
                        block[i] = id;
                    }
                    else
                    {
                        block[i] = 0;
                    }
                }
            }
            windowList = new Dictionary<int, int[]>();
        }

        public bool CheckWindowEmpty(int begin, int end)
        {

            bool isEmpty = true;
            if (begin > end)
            {
                end = end + length;
            }
            for (int i = begin; i <= end; i++)
            {
                //Debug.Log(i);
                if (block[i%length] != 0)
                {
                    isEmpty = false;
                }
            }                
            return isEmpty;
        }

        public int[] CheckMoveBoundary(int begin, int end)
        {
            int[] bounds = new int[2];
            
            if (block[begin] == 0)
            {
                bounds[0] = begin;
            }
            else
            {
                int beginBlock = block[begin];
                while (beginBlock == block[begin])
                {
                    begin = (begin - 1 + length)%length;
                }
                bounds[0] = (begin + 1 + length) % length;
            }
            //Debug.Log(end);
            if (block[end % length] == 0)
            {
                bounds[1] = end % length;
            }
            else
            {
                int beginBlock = block[end % length];
                while (beginBlock == block[end % length])
                {
                    end = (end + 1) % length;
                }
                bounds[1] = (end - 1) % length;
            }
            //Debug.Log("MoveBoundary begin: " + bounds[0] + " end: " + bounds[1]);
            return bounds;
        }

        public void SetWindowId(int begin, int end, int id)
        {
            if (begin > end)
            {
                end = end + length;
            }
            for (int i = begin; i <= end; i++)
            {
                block[i % length] = id;
            }
            windowList.Add(id, new int[2] { begin, end });
            Debug.Log("insert window with id " + id);
        }

        public void SetWindowId(int begin, int end, int[] id)
        {
            if (begin > end)
            {
                end = end + length;
            }
            int tempBegin = begin;
            int tempEnd = end;
            int tempId = id[0];
            for (int i = begin; i <= end; i++)
            {
                block[i % length] = id[i - begin];
                if(tempId!= id[i - begin])
                {
                    tempEnd = (i - 1) % length;
                    if(id[i - begin - 1] != 0)
                    {
                        windowList.Add(id[i - begin - 1], new int[2] { tempBegin, tempEnd });
                        Debug.Log("insert window with id " + tempId);
                    }  
                    tempBegin = i % length;
                    tempId = id[i - begin];

                }
            }
            if (id[end-begin] != 0)
            {
                tempEnd = end % length;
                windowList.Add(id[end - begin], new int[2] { tempBegin, tempEnd });
                //Debug.Log("insert window with id " + tempId);
            }

        }

        public void RemoveWindowId(int begin, int end)
        {
            if (begin > end)
            {
                end = end + length;
            }
            List<int> removeId = GetWindowIdList(begin, end);
            foreach (int windowid in removeId)
            {
                windowList.Remove(windowid);
                //Debug.Log("remove window with id " + windowid);
            }
            for (int i = begin; i <= end; i++)
            {
                block[i % length] = 0;
            }
        }

        public int[] GetWindowId(int begin, int end)
        {
            if (begin > end)
            {
                end = end + length;
            }
            int[] bounds = new int[end - begin + 1];
            for (int i = begin; i <= end; i++)
            {
                bounds[i-begin] = block[i % length];
            }
            //Debug.Log("WindowId begin: " + begin + " end: " + end);

            return bounds;
        }

        public List<int> GetWindowIdList(int begin, int end)
        {
            if (begin > end)
            {
                end = end + length;
            }
            List<int> IdList = new List<int>();
            for (int i = begin; i <= end; i++)
            {
                if(!IdList.Exists(t => t == block[i % length])&& block[i % length]!=0)
                    IdList.Add(block[i % length]);
            }
            return IdList;
        }

        public bool WindowExist(int id)
        {
            return windowList.ContainsKey(id);
        }

        public int[] WindowPosition(int id)
        {
            return windowList[id];
        }

        public bool CheckSpaceEmpty()
        {
            return (windowList.Count == 0);
        }

        public static SpaceBase SetSpace()
        {
            if (instance = null)
            {
                instance = new SpaceBase();
            }
            return instance;
        }

        
    }
}