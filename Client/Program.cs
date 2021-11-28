using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("Start...");

            var ip = "127.0.0.1";
            var port = 8005;
            var ipServer = new IPEndPoint(IPAddress.Parse(ip), port);
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipServer);
            Console.WriteLine("Connect to Server");
            
            Console.Write("Введите сообщение: ");
            var msg = Console.ReadLine();
            socket.Send(Encoding.Unicode.GetBytes(msg));
            Console.WriteLine("Send...");
            
            var buffer = new byte[256];
            var data = new List<byte>();
            do
            {
                socket.Receive(buffer);
                data.AddRange(buffer);
            } while (socket.Available > 0);

            var t = data.ToArray();
            msg = Encoding.Unicode.GetString(t, 0, t.Length);
            Console.WriteLine($"Ответ от сервера - {msg}");
            
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}