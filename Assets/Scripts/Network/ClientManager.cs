using System.Threading.Tasks;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    private Client client;

    public Client Client { get => client; set => client = value; }

    public void ConnectToServer()
    {
        client = new Client();
        client.Connect("127.0.0.1", 12345);
        Debug.Log("Подключение к серверу.");
        client.SendData("Привет, сервер!");
        string serverMessage = client.ReceiveData();
        Debug.Log("Сообщение от сервера: " + serverMessage);
    }

    void OnApplicationQuit()
    {
        client.Disconnect();
        Debug.Log("Отключение от сервера.");
    }

    public void SendAbilityChoice(int abilityIndex, Color effectColor)
    {
        client.SendAbilityChoice(abilityIndex, effectColor);
    }

    public async Task<string> ReceiveServerResponseAsync()
    {
        string response = await Task.Run(() => client.ReceiveData());
        Debug.Log("Ответ сервера: " + response);
        return response;
    }

    public void UpdateUnitHealth(Unit unit, int healthChange)
    {
        unit.currentHealth += healthChange;
        Debug.Log("Здоровье юнита обновлено: " + unit.currentHealth);
    }
}