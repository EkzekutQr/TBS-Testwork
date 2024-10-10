using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Server
{
    private TcpListener listener;
    private TcpClient client;
    private NetworkStream stream;

    public void Start(int port)
    {
        listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
    }

    public void WaitForClient()
    {
        client = listener.AcceptTcpClient();
        stream = client.GetStream();
    }

    public string ReceiveData()
    {
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        return Encoding.UTF8.GetString(buffer, 0, bytesRead);
    }

    public void SendData(string data)
    {
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        stream.Write(dataBytes, 0, dataBytes.Length);
    }

    public void Stop()
    {
        stream.Close();
        client.Close();
        listener.Stop();
    }

    public Color ConvertStringToColor(string colorString)
    {
        string[] rgba = colorString.Split(',');
        float r = float.Parse(rgba[0]);
        float g = float.Parse(rgba[1]);
        float b = float.Parse(rgba[2]);
        float a = float.Parse(rgba[3]);
        return new Color(r, g, b, a);
    }
}