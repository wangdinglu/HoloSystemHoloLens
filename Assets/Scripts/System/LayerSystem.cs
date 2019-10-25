using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MixOne
{
    public class LayerSystem : MonoBehaviour
    {
        private static Dictionary<string, GameObject> Layers;

        public List<string> LayerList = new List<string>{
            "Layers","SystemLayer","InfoLayer","ApplicationLayer","StaticLayer","DynamicLayer","Background"
        };

        private void Awake()
        {
            Layers = new Dictionary<string, GameObject>();

            foreach(string layer in LayerList){
                GameObject item = GameObject.Find(layer);
                //Debug.Log(layer);
                //Debug.Log(item.name);

                Layers.Add(layer, item);
            }
        }
        
        public GameObject GetLayer(string layer)
        {
            return Layers[layer];
        }

    }
}