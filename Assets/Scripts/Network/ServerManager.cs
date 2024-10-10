using UnityEngine;

public class ServerManager : MonoBehaviour
{
    private Server server;

    void Start()
    {
        server = new Server();
        server.Start(12345);
        Debug.Log("������ �������.");
    }

    void Update()
    {
        server.WaitForClient();
        Debug.Log("������ ���������.");
        string clientMessage = server.ReceiveData();
        Debug.Log("��������� �� �������: " + clientMessage);
        string[] data = clientMessage.Split(',');
        int abilityIndex = int.Parse(data[0]);
        Color effectColor = server.ConvertStringToColor(data[1]);
        // ��������� ������ ����������� � ����� �������
        server.SendData("������, ������!");
    }

    void OnApplicationQuit()
    {
        server.Stop();
        Debug.Log("������ ����������.");
    }
}