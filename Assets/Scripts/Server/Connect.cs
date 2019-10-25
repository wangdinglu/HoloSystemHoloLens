using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MixOne
{
    public class Connect : MonoBehaviour
    {
        private Button button;
        private InputField IP;

        // Start is called before the first frame update
        void Start()
        {
            button = gameObject.GetComponent<Button>();
            button.onClick.AddListener(sendInfo);
            IP = GameObject.Find("IP").GetComponent<InputField>();
        }

        public void sendInfo()
        {
            string name = button.name;
            Debug.Log(button.name);
            SpaceSettings.serverIP = IP.text;
            SceneManager.LoadScene("AR_02");
        }


    }
}