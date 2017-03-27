using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace 资源管理器
{
    public class AsyncTCPClient
    {
        public void AsynConnect()
        {
            //端口以及ip
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9095);
            //创建套接字
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //连接到服务器
            client.BeginConnect(ipe, asyncResult =>
             {
                 client.EndConnect(asyncResult);
                 //向服务器发送信息
                 AsynSend(client, "你好我是客户端");
                 AsynSend(client, "第一次发送");
                 AsynSend(client, "第二次发送");
                 //接收消息
                 AsynRecive(client);

             }, null);
        }
        private void AsynSend(Socket socket, string message)
        {
            if (socket == null || message == string.Empty) return;
                byte[] data = Encoding.UTF8.GetBytes(message);
                try
                {
                    socket.BeginSend(data, 0, data
                        .Length, SocketFlags.None, asyncResult =>
                          {
                              int length = socket.EndSend(asyncResult);
                              Console.WriteLine(string.Format("客户端发送信息: {0}", message));
                          }
                           , null);
                }
                catch (Exception e)
                {
                    Console.WriteLine("异常信息: { 0}",e.Message);
                }
        }

        public void AsynRecive(Socket socket)
        {
            byte[] data = new byte[1024];
            try
            {
                //开始接受数据
                socket.BeginReceive(data, 0, data.Length, SocketFlags.None,
                asyncResult =>
                {
                    int length = socket.EndReceive(asyncResult);
                    Console.WriteLine(string.Format("收到服务器消息:{0}", Encoding.UTF8.GetString(data)));
                    AsynRecive(socket);
                }, null);

            }
            catch (Exception ex)
            {
                Console.WriteLine("异常信息: {0}", ex.Message);
            }
        }

    }
}
