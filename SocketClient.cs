using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RazielSignal {
    class SocketClient
    {
        int Port { get; set; }
        string Ip { get; set; }
        static IPAddress ipAddress;
        static IPEndPoint remoteEP;
        static Socket sender;
        static byte[] bytes = new byte[1024];

        public SocketClient(string ip, int port) {
            Ip = ip;
            Port = port;
        }

        /// <summary>
        /// Hace conexion con el Socket (Metodo asincrono).
        /// </summary>
        /// <returns></returns>
        public async Task<bool> fnConnectSocketAsync() {
            try {
                return await Task.Run(() => fnConnectSocket());
            } catch { }
            return false;
        }

        /// <summary>
        /// Manda informacion al Socket (Metodo asincrono).
        /// </summary>
        /// <param name="msg"></param>
        public async void fnSendInfoAsync(string msg) {
            try {
                await Task.Run(() => fnSendInfo(msg));
            } catch { }
        }

        /// <summary>
        /// Recibe informacion del Socket (Metodo asincrono).
        /// </summary>
        /// <returns></returns>
        public async Task<List<File>> fnReceiveInfoAsync() {
            try {
                return await Task.Run(() => fnReceiveInfo());
            } catch { }
            return null;
        }

        /// <summary>
        /// Hace conexion con el Socket (Metodo sincrono).
        /// </summary>
        /// <returns></returns>
        public bool fnConnectSocket() {
            try {
                ipAddress = IPAddress.Parse(Ip);
                remoteEP = new IPEndPoint(ipAddress, Port);
                sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(remoteEP);
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        /// <summary>
        /// Manda informacion al Socket (Metodo sincrono).
        /// </summary>
        /// <param name="message"></param>
        public void fnSendInfo(string message) {
            try {
                byte[] msg = Encoding.ASCII.GetBytes(message + "\n");
                //byte[] msg = Encoding.Unicode.GetBytes(message + "<EOF>");
                int bytesSent = sender.Send(msg);
            } catch (Exception ex) {
            }
        }

        /// <summary>
        /// Recibe informacion del Socket (Metodo sincrono).
        /// </summary>
        /// <returns></returns>
        public List<File> fnReceiveInfo() {
            try {
                List<File> result = new List<File>();
                int bytesRec = sender.Receive(bytes);
                string value = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                string[] vs = value.Split('-');
                if (!vs[0].Contains("Empty")) {
                    for (int i = 0; i < vs.Length - 1; i++) {
                        string[] vs2 = vs[i].Split(',');
                        result.Add(new File { Name = vs2[0], CreationDate = Convert.ToDateTime(vs2[1]), Type = vs2[2], Path = vs2[3] });
                    }
                    return result;
                } else
                    return null;
            } catch (Exception ex) {
                return null;

            }
        }

        /// <summary>
        /// Cierra la conexion con el Socket.
        /// </summary>
        public void fnClose() {
            try {
                sender.Close();
                sender.Dispose();
            } catch { }
        }
    }
}
