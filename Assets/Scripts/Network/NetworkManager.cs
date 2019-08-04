using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

// request rules <request size>&<request type>&<(propname)=(propvalue)...>
class NetworkManager : MonoBehaviour
{
    public bool IsServer = false;
    public bool UseLocalHost = false;
    public int ServerPost = 21034;
    public int MaxPlayers = 12;
    private Socket m_listener = null;

    public enum Requests
    {
        SyncPlayers,
        SyncPlayerCards,
        SyncPlayerDices,
    }

    public void Start()
    {
        if (IsServer)
        {
            HostServer();
        }
    }

    private void HostServer()
    {
        IPHostEntry iPHost = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddr;
        Debug.Log(iPHost.AddressList[0]);
        if (UseLocalHost)
        {
            ipAddr = IPAddress.Loopback;
        }
        else
        {
            ipAddr = IPAddress.Loopback;
        }
        Debug.Log(ipAddr);
        IPEndPoint localEndPoint = new IPEndPoint(ipAddr, ServerPost);

        m_listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        m_listener.Bind(localEndPoint);

        m_listener.Listen(MaxPlayers);

        Debug.Log("Hosted");

        var e = new SocketAsyncEventArgs();
        bool sockEnable = m_listener.AcceptAsync(e);
        e.Completed += E_Completed;
    }

    private void ConnectToServer()
    {

    }

    void Update()
    {
        if (IsServer && m_listener != null)
        {
        }
    }
    NetworkUnit newUnit;
    private void E_Completed(object sender, SocketAsyncEventArgs e)
    {
        var b = new SocketAsyncEventArgs();
        bool sockEnable = m_listener.AcceptAsync(b);
        b.Completed += E_Completed;

        newUnit = new NetworkUnit(e.AcceptSocket);
        newUnit.SendMessage("shit\n\n");
    }
}
