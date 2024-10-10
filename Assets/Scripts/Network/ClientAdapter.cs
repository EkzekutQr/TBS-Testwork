using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientAdapter : INetworkAdapter
{
    private Client client;

    public ClientAdapter(Client client)
    {
        this.client = client;
    }

    public void SendData(string data)
    {
        client.SendData(data);
    }

    public string ReceiveData()
    {
        return client.ReceiveData();
    }
}