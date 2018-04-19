using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.Client;

// SocketAPI class
// Created: TM Quan 19/4
// This class is use for connecting with server

namespace HyberShift_CSharp.Utilities
{
    public class SocketAPI
    {
        private static Socket socket;
        private static SocketAPI instance;
        private static bool isConnected;

        public SocketAPI()
        {
            socket = IO.Socket("http://hybershift-server-helloqwert12.c9users.io/");
            isConnected = false;
        }

        public static SocketAPI GetInstance()
        {
            if (instance == null)
                instance = new SocketAPI();
            return instance;
        }

        public Socket GetSocket()
        {
            return socket;
        }

        public void Connect()
        {
            if (!isConnected)
            {
                socket.Connect();
                isConnected = true;
                this.EstablishConnection();
            }
        }

        public void Disconnect()
        {
            if (isConnected)
            {
                socket.Disconnect();
                isConnected = false;
            }
        }

        public void EstablishConnection()
        {
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                Debug.Log("Client connected to server");
            }).On(Socket.EVENT_DISCONNECT, () =>
            {
                Debug.Log("Client disconnected to server");
            });
        }

        // delegate to create event
        //public void AddEvent(string eventName, Action method)
        //{
        //    socket.On(eventName, method);
        //}

        //public void AddEvent(string eventName, Action<object> method)
        //{
        //    socket.On(eventName, method);
        //}
    }
}
