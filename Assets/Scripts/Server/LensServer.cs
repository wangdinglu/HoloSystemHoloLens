using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
//using MiniJSON;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;

//using System.Threading;

using UnityEngine;
#if UNITY_UWP
    using Windows.Foundation;
    using Windows.Networking.Sockets;
    using Windows.Security.Cryptography.Certificates;
    using Windows.Storage.Streams;
    using System.Threading.Tasks;
#else
using System.Text;
#endif


namespace MixOne
{
    public class LensServer : MonoBehaviour
    {
        // Use this for initialization
#if UNITY_UWP
    private MessageWebSocket messageWebSocket;
    private DataWriter messageWriter;
#endif
        int random = 0;
        private Text connect;
        private bool check = false;
        int count = 0;
        private ControlList cl;
        Color[] colorArray = { Color.white, Color.yellow, Color.blue, Color.black, Color.red, Color.green, Color.grey };
        string[] stringArray = { "white", "yellow", "blue", "black", "red", "green", "grey" };

        private bool rotate = false;
        private Vector3 v = new Vector3(0, 5, 0);

        void Start()
        {
            cl = GameObject.Find("StatusControl").GetComponent<ControlList>();
#if UNITY_UWP
        // HoloLens螳滓ｩ溘〒WebSocket謗･邯夐幕蟋�
        OnConnect("Start");
#endif
        }

        private void Update()
        {
            if(rotate)
                this.transform.Rotate(v);
            //Debug.Log(nm.ReadByte());
        }
    
    // Called by SpeechManager when the user says the "Reset world" command
    void OnReset()
    {

    }

    // Called by SpeechManager when the user says the "Drop sphere" command
    void OnDrop()
    {

    }

#if UNITY_UWP
 
 
    private void OnConnect(string json)
    {

        AppendOutputLine("OnConnect");

        messageWebSocket = new MessageWebSocket();

        //In this case we will be sending/receiving a string so we need to set the MessageType to Utf8.
        messageWebSocket.Control.MessageType = SocketMessageType.Utf8;

        //Add the MessageReceived event handler.
        messageWebSocket.MessageReceived += WebSock_MessageReceived;

        //Add the Closed event handler.
        messageWebSocket.Closed += WebSock_Closed;

        Uri serverUri = new Uri("ws://192.168.3.72:8080"); // 蛻･PC縺ｮNode-RED縺ｮWebSocket縺ｫ縺､縺ｪ縺後ｋ

        try
        {
            Task.Run(async () => {
                //Connect to the server.
                AppendOutputLine("Connect to the server...." + serverUri.ToString());
                await Task.Run(async () =>
                {
                    await messageWebSocket.ConnectAsync(serverUri);
                    AppendOutputLine("ConnectAsync OK");

                    await WebSock_SendMessage(messageWebSocket, json);

                    //譁�ｭ励ｒ陦ｨ遉ｺ縺吶ｋ
                    connect.text = "start";

                });

            });
        }
        catch (System.Exception ex)
        {
            AppendOutputLine("error : " + ex.ToString());
            connect.text = "error";
            //Add code here to handle any exceptions
        }

    }
     
    private async Task WebSock_SendMessage(MessageWebSocket webSock, string message)
    {
        AppendOutputLine("WebSock_SendMessage : " + message);
 
        DataWriter messageWriter = new DataWriter(webSock.OutputStream);
        messageWriter.WriteString(message);
        await messageWriter.StoreAsync();
    }
 
    private void WebSock_MessageReceived(MessageWebSocket sender, MessageWebSocketMessageReceivedEventArgs args)
    {
        DataReader messageReader = args.GetDataReader();
        messageReader.UnicodeEncoding = UnicodeEncoding.Utf8;
        string messageString = messageReader.ReadString(messageReader.UnconsumedBufferLength);
        AppendOutputLine("messageString : " + messageString);

        //Add code here to do something with the string that is received.

        Task.Run(async () =>
        {

            UnityEngine.WSA.Application.InvokeOnAppThread(() => {

                cl.pushOperation(messageString);

            }, true);

            await Task.Delay(100);
        });
    }

    private void WebSock_Closed(IWebSocket sender, WebSocketClosedEventArgs args)
    {
        //Add code here to do something when the connection is closed locally or by the server
        connect.text = "close";
    }

    private void AppendOutputLine(string value)
    {
        // OutputField.Text += value + "\r\n";
        Debug.Log(value);
    }

 
#endif

    }
}