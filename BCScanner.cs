using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace BCS
{
    public class BCScanner:INotifyPropertyChanged
    {
        public TcpClient client = new TcpClient();
        public NetworkStream stream = null;
        byte[] recvData = new byte[50 * 10];

        private string _bcData;
        public string BCData
        {
            get
            {
                return _bcData;
            }
            set
            {
                _bcData = value;
                Debug.WriteLine(_bcData);
                OnPropertyChanged(nameof(BCData));
            }
        }        

        public void TCPClientStart()
        {                
            client.Connect("192.168.100.100",6023);
            stream = client.GetStream();
            Thread th = new Thread(TCPClientListener);
            th.Start();           
        }

        public void TCPClientWrite(string buffer)
        {
            byte[] outBound = Encoding.ASCII.GetBytes(buffer);
            stream.Write(outBound, 0, outBound.Length);
            stream.Flush();
        }

        public void TCPClientListener()
        {
            
            while (true)
            {
                int bufSize = client.ReceiveBufferSize;               
                int count = stream.Read(recvData, 0, 100);
                BCData = Encoding.ASCII.GetString(recvData, 0, count);
            }
        }
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
