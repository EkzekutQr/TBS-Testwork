using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Client
{
    private TcpClient client;
    private NetworkStream stream;

    public void Connect(string serverAddress, int port)
    {
        client = new TcpClient(serverAddress, port);
        stream = client.GetStream();
    }

    public void SendData(string data)
    {
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        stream.Write(dataBytes, 0, dataBytes.Length);
    }

    public string ReceiveData()
    {
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        return Encoding.UTF8.GetString(buffer, 0, bytesRead);
    }

    public void Disconnect()
    {
        stream.Close();
        client.Close();
    }

    public string ConvertColorToString(Color color)
    {
        return $"{color.r},{color.g},{color.b},{color.a}";
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

    public void SendAbilityChoice(int abilityIndex, Color effectColor)
    {
        string colorString = ConvertColorToString(effectColor);
        SendData($"Выбор способности: {abilityIndex},{colorString}");
    }
}
