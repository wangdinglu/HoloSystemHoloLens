using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MixOne
{
    public class SpaceCalculation : SpaceSettings
    {
        public int[] GetBounds(Vector3 view,int width)
        {
            int[] bounds = new int[2];
            
            bounds[0] = Mathf.FloorToInt(view.y / cell - (float)width / 2);
            bounds[1] = Mathf.FloorToInt(view.y / cell + (float)width / 2);
            if (bounds[0] < 0)
                bounds[0] += length;
            if (bounds[1] < 0)
                bounds[1] += length;
            return bounds;
        }

        public Vector3 SetDynamicPosition(Vector3 view, int layer)
        {
            float angle = view.y/180*Mathf.PI;
            float factor = Mathf.Pow((layerDynamicRadius / basicDynamicRadius + 1), layer);
            float radius = basicDynamicRadius * factor;
            Vector3 position = new Vector3(radius*Mathf.Sin(angle),0, radius * Mathf.Cos(angle));
            return position;
        }

        public Vector3 SetStaticPosition(int layer)
        {
            float factor = Mathf.Pow((layerStaticRadius / basicStaticRadius + 1), layer);
            float radius = basicStaticRadius * factor;
            Vector3 position = new Vector3(0, 0, radius);
            return position;
        }

        public Vector3 SetDynamicPosition(Vector3 oldPosition)
        {
            Vector3 position = oldPosition * (layerDynamicRadius / basicDynamicRadius + 1);
            return position;
        }

        public Vector3 SetStaticPosition(Vector3 oldPosition)
        {
            Vector3 position = oldPosition * (layerStaticRadius / basicStaticRadius + 1);
            return position;
        }

        public Vector3 SetRotation(Vector3 view)
        {
            Vector3 rotation = new Vector3(0, view.y, 0);
            return rotation;
        }

        public Vector3 SetDynamicScale(int layer)
        {
            float scaleFactor = Mathf.Pow((layerDynamicRadius / basicDynamicRadius + 1),layer);
            Vector3 scale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            return scale;
        }

        public Vector3 SetStaticScale(int layer)
        {
            float scaleFactor = Mathf.Pow((layerStaticRadius / basicStaticRadius + 1), layer);
            Vector3 scale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            return scale;
        }

        public bool CheckInsert(int[] id)
        {
            bool isEmpty = true;
            for(int i = 0; i < id.Length; i++)
            {
                if (id[i] != 0)
                {
                    isEmpty = false;
                    return isEmpty;
                }
            }
            return isEmpty;
        }
    }
}

