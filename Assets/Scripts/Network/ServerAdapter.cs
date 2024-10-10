using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerAdapter : INetworkAdapter
{
    private Server server;

    public ServerAdapter(Server server)
    {
        this.server = server;
    }

    public void SendData(string data)
    {
        server.SendData(data);
    }

    public string ReceiveData()
    {
        return server.ReceiveData();
    }
}