using System.Threading.Tasks;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    private Client client;
    private INetworkAdapter clientAdapter;

    public Client Client { get => client; set => client = value; }

    public void ConnectToServer()
    {
        client = new Client();
        client.Connect("127.0.0.1", 12345);
        clientAdapter = new ClientAdapter(client);
        Debug.Log("Подключение к серверу.");
        clientAdapter.SendData("Привет, сервер!");
        string serverMessage = clientAdapter.ReceiveData();
        Debug.Log("Сообщение от сервера: " + serverMessage);
    }

    void OnApplicationQuit()
    {
        client.Disconnect();
        Debug.Log("Отключение от сервера.");
    }

    public void SendAbilityChoice(int abilityIndex, Color effectColor)
    {
        string data = $"{abilityIndex},{ConvertColorToString(effectColor)}";
        clientAdapter.SendData(data);
    }

    public async Task<string> ReceiveServerResponseAsync()
    {
        string response = await Task.Run(() => clientAdapter.ReceiveData());
        Debug.Log("Ответ сервера: " + response);
        return response;
    }

    private string ConvertColorToString(Color color)
    {
        return $"{color.r},{color.g},{color.b},{color.a}";
    }

    private Color ConvertStringToColor(string colorString)
    {
        string[] rgba = colorString.Split(',');
        float r = float.Parse(rgba[0]);
        float g = float.Parse(rgba[1]);
        float b = float.Parse(rgba[2]);
        float a = float.Parse(rgba[3]);
        return new Color(r, g, b, a);
    }
}
