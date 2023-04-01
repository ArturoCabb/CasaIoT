using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CasaIoT
{
    internal class SocketThread
    {
        private readonly string IPAddress = "192.168.1.70";
        private readonly int port = 8080;

        public void Start(string mensaje)
        {
            //Thread thread = new Thread(new ParameterizedThreadStart(Run));
            //thread.Start(mensaje);
            Thread thread = new Thread(() => Run(mensaje));
            thread.Start();
        }

        private void Run(string mensaje)
        {
            try
            {
                TcpClient tcpClient = new TcpClient(IPAddress, port);
                NetworkStream stream = tcpClient.GetStream();
                StreamReader sr = new StreamReader(stream, Encoding.UTF8);
                StreamWriter sw = new StreamWriter(stream, Encoding.UTF8);

                enviarMensaje(sw, mensaje);
                string mensajeRecibido = recibirMensaje(sr);
                Console.WriteLine(mensajeRecibido);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void enviarMensaje(StreamWriter streamWriter ,string mensaje)
        {
            streamWriter.WriteLine(mensaje);
            streamWriter.Flush();
        }

        private string recibirMensaje(StreamReader streamReader)
        {
            return streamReader.ReadLine();
        }
    }
}
