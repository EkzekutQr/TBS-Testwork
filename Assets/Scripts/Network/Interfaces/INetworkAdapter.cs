using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INetworkAdapter
{
    void SendData(string data);
    string ReceiveData();
}
