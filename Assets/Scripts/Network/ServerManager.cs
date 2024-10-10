using UnityEngine;

public class ServerManager : MonoBehaviour
{
    private Server server;

    void Start()
    {
        server = new Server();
        server.Start(12345);
        Debug.Log("Сервер запущен.");
    }

    void Update()
    {
        server.WaitForClient();
        Debug.Log("Клиент подключен.");
        string clientMessage = server.ReceiveData();
        Debug.Log("Сообщение от клиента: " + clientMessage);
        string[] data = clientMessage.Split(',');
        int abilityIndex = int.Parse(data[0]);
        Color effectColor = server.ConvertStringToColor(data[1]);
        // Обработка выбора способности и цвета эффекта
        server.SendData("Привет, клиент!");
    }

    void OnApplicationQuit()
    {
        server.Stop();
        Debug.Log("Сервер остановлен.");
    }
}