﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    static class Program
    {
        static void Main()
        {
            var ip = "127.0.0.1";
            var port = 8005;

            var ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listen.Bind(ipEndPoint);
            listen.Listen(10);

            while (true)
            {
                var connect = listen.Accept();

                var buffer = new byte[256];
                var data = new List<byte>();

                do
                {
                    connect.Receive(buffer);
                    data.AddRange(buffer);
                } while (connect.Available > 0);

                var t = data.ToArray();
                var msg = Encoding.Unicode.GetString(t, 0, t.Length);
                
                Console.WriteLine(msg);
                
                connect.Send(Encoding.Unicode.GetBytes($"Ваше сообщение: {msg}"));
                
                connect.Shutdown(SocketShutdown.Both);
                connect.Close();
            }
        }
    }
}