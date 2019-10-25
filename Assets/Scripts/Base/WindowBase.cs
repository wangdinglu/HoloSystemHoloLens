using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MixOne
{
    public class WindowBase : MonoBehaviour
    {
        

        private static WindowBase instance = null;
        public string AppName { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public int Id { get; set; }

        public WindowBase() { }
        public WindowBase(string _name, float _width, float _height,int _id)
        {
            AppName = _name;
            Width = _width;
            Height = _height;
            Id = _id;
        }

        public static WindowBase SetWindow()
        {
            if (instance = null)
            {
                instance = new WindowBase();
            }
            return instance;
        }

    }
}